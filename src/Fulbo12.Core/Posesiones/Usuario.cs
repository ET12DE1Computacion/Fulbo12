using System.Diagnostics.CodeAnalysis;
using Fulbo12.Core.Futbol;

namespace Fulbo12.Core.Posesiones;
public class Usuario : PersonaBase
{
    public static readonly uint MonedasMaximas = 9_999_999;
    public static readonly byte LimitePosesiones = 100;
    public static readonly string _futbolistaEnPosesion
        = "Futbolista ya se encuentra en posesión";
    public static readonly string _noEstaEnNovedades
        = "No se encuentra posesion entre las novedades";
    public static readonly string _limitePosesiones
        = "Ya alcanzó el limite para posesiones";
    public static readonly string _noPoseeMonedasSuficientes
        = "No se poseee la cantidad de monedas suficientes";
    public static readonly string _mismoVendedor
        = "No se puede ofertar por Publicaciones propias";
    public static readonly string _ofertaMenor
        = "No se puede ofertar por debajo";
    public required string Email { get; set; }
    public uint Monedas { get; private set; }
    public required List<Posesion> NuevasPosesiones { get; init; }
    public required List<Posesion> Posesiones { get; init; }
    public required List<Posesion> Transferibles { get; init; }
    public required List<Publicacion> Publicaciones { get; init; }
    public Usuario() { }
    [SetsRequiredMembers]
    public Usuario(short id, string nombre, string apellido, DateOnly nacimiento, Pais pais, string email)
        : base (id, nombre, apellido, nacimiento, pais)
    {
        Email = email;
        Monedas = 0;
        NuevasPosesiones = new List<Posesion>();
        Posesiones = new List<Posesion>();
        Transferibles = new List<Posesion>();
        Publicaciones = new List<Publicacion>();
    }
    public bool PuedeAgregar
        => Transferibles.Count + Publicaciones.Count < LimitePosesiones;
    public bool PoseeFutbolista(Futbolista futbolista)
        => Posesiones.Exists(p => p.EsFutbolista(futbolista));
    public void AgregarNovedad(Posesion posesion) => NuevasPosesiones.Add(posesion);
    public void AgregarPosesion(Posesion posesion)
    {
        if (!PoseeFutbolista(posesion.Futbolista))
        {
            try
            {
                RemoverPosesionDeNovedad(posesion);
                Posesiones.Add(posesion);
            }
            catch (InvalidOperationException e)
            {
                throw e;
            }
        }
        else
            throw new InvalidOperationException(_futbolistaEnPosesion);
    }
    public void AgregarTransferible(Posesion posesion)
    {
        if (PuedeAgregar)
        {
            if (NuevasPosesiones.Remove(posesion))
                Transferibles.Add(posesion);
        }
        else
            throw new InvalidOperationException(_limitePosesiones);
    }
    private bool RemoverPosesionDeNovedad(Posesion posesion)
    {
        if (NuevasPosesiones.Remove(posesion))
            return true;
        throw new InvalidOperationException(_noEstaEnNovedades);
    }
    public void Publicar(Publicacion publicacion)
    {
        if (Transferibles.Remove(publicacion.Posesion))
            Publicaciones.Add(publicacion);
    }
    public bool TieneAlMenos(uint monedas) => Monedas >= monedas;
    public void Ofertar(Publicacion publicacion, uint oferta)
    {
        if (oferta < publicacion.OfertaMinima)
            throw new InvalidOperationException(_ofertaMenor);
        if (!TieneAlMenos(publicacion.OfertaOMinima))
            throw new InvalidOperationException(_noPoseeMonedasSuficientes);
        if (publicacion.EsVendedor(this))
            throw new InvalidOperationException(_mismoVendedor);
        Debitar(oferta);
        publicacion.RecibirOferta(this, oferta);
    }
    public void SacarOferta(Publicacion publicacion) => Acreditar(publicacion.Oferta!.Value);
    public void Acreditar(uint monedas)
        => Monedas = Monedas + monedas > MonedasMaximas ? MonedasMaximas : Monedas + monedas;
    public void BajarPublicacion(Publicacion publicacion)
        => Publicaciones.Remove(publicacion);
    public void Debitar(uint monedas) => Monedas -= monedas;
}