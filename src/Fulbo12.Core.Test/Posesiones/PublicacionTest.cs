using Fulbo12.Core.Posesiones.Fixtures;

namespace Fulbo12.Core.Posesiones.Test;
[Trait("Category", "Posesion")]
public class PublicacionTest : IClassFixture<PosesionesFixture>
{
    public Usuario Arturo { get; set; }
    public Usuario Lucho { get; set; }
    public Usuario Beymar { get; set; }
    public Posesion LuchoPosesion { get; set; }
    public Publicacion Publicacion { get; set; }
    private static readonly uint _minima = 500;
    private static readonly uint _compra = 5000;
    public PublicacionTest(PosesionesFixture pf)
    {
        Arturo = pf.Usuarios.Arturo;
        Lucho = pf.Usuarios.Lucho;
        LuchoPosesion = new Posesion(Lucho, pf.Futbolistas.FMarcosRojo);
        pf.BlanquearUsuario(Lucho);
        pf.BlanquearUsuario(Arturo);
        Lucho.AgregarNovedad(LuchoPosesion);
        Lucho.AgregarTransferible(LuchoPosesion);
        Publicacion = new Publicacion(LuchoPosesion, _minima, _compra, dias: 3);
        Lucho.Publicar(Publicacion);
        Beymar = new Usuario(0,"Beymar", "Leon", new DateOnly(2004, 10, 10), Lucho.Pais, "bl@gmail.com");
    }

    [Fact]
    public void Constructor()
    {
        Assert.Null(Publicacion.Ofertante);
        Assert.Null(Publicacion.Oferta);
        Assert.Equal(_minima, Publicacion.OfertaOMinima);
        Assert.False(Publicacion.HayOfertante);
        Assert.True(Publicacion.CantidadEsMayorOIgual(_minima));
        Assert.False(Publicacion.CantidadEsMayorOIgual(_minima - 1));
    }

    [Fact]
    public void ArturoOfertaSolo()
    {
        Arturo.Acreditar(_minima);

        Arturo.Ofertar(Publicacion, Arturo.Monedas);

        Assert.Same(Arturo, Publicacion.Ofertante);
        Assert.Equal(_minima, Publicacion.OfertaOMinima);
        Assert.Equal<uint>(0, Arturo.Monedas);
        Assert.True(Publicacion.HayOfertante);
    }

    [Fact]
    public void BeymarNoAlcanza()
    {
        Arturo.Acreditar(_minima);
        Beymar.Acreditar(_minima - 1);
        Arturo.Ofertar(Publicacion, Arturo.Monedas);

        var ex = Assert.Throws<InvalidOperationException>
            (() => Beymar.Ofertar(Publicacion, Beymar.Monedas));
        Assert.Equal(Usuario._ofertaMenor, ex.Message);

        Publicacion.RecibirOferta(Beymar, Beymar.Monedas);
        Assert.Same(Arturo, Publicacion.Ofertante);
        Assert.Equal(_minima, Publicacion.OfertaOMinima);
    }

    [Fact]
    public void BeymarSuperaArturo()
    {
        Arturo.Acreditar(_minima);
        Beymar.Acreditar(_minima + 1);
        Arturo.Ofertar(Publicacion, Arturo.Monedas);
        Assert.Equal<uint>(0, Arturo.Monedas);

        Beymar.Ofertar(Publicacion, Beymar.Monedas);

        //Ahora Beymar es el nuevo ofertante
        Assert.Same(Beymar, Publicacion.Ofertante);
        Assert.Equal(_minima + 1, Publicacion.OfertaOMinima);
        //Arturo recupero sus monedas
        Assert.Equal(_minima, Arturo.Monedas);
        Assert.Equal<uint>(0, Beymar.Monedas);
    }

    [Fact]
    public void AplicarSinOfertante()
    {
        Assert.Empty(Lucho.Transferibles);
        Assert.Single(Lucho.Publicaciones);

        Publicacion.Aplicar();

        Assert.Empty(Lucho.Publicaciones);
        Assert.Single(Lucho.Transferibles);
    }

    [Fact]
    public void AplicarConOfertante()
    {
        Assert.Equal<uint>(0, Lucho.Monedas);
        Arturo.Acreditar(_minima);
        Arturo.Ofertar(Publicacion, Arturo.Monedas);
        Assert.Empty(Arturo.NuevasPosesiones);

        Publicacion.Aplicar();

        //Lucho gana monedas por la venta
        Assert.Equal(_minima, Lucho.Monedas);
        //Se incrementó la cantidad de dueños
        Assert.Equal(2, Publicacion.Posesion.Duenos);
        Assert.Single(Arturo.NuevasPosesiones);
    }
}