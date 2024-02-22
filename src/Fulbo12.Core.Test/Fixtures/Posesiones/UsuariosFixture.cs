using Fulbo12.Core.Fixtures;

namespace Fulbo12.Core.Posesiones.Fixtures;
public class UsuariosFixture
{
    public Usuario Lucho { get; set; }
    public Usuario Arturo { get; set; }
    private PaisesFixtures _pf;
    public UsuariosFixture(PaisesFixtures? pf = null)
    {
        _pf = pf ?? new PaisesFixtures();

        Lucho = new Usuario(1, "Luis", "Duran",
            nacimiento: new DateOnly(2000, 10, 5),
            pais: _pf.Argentina, @"123@tmail.com");

        Arturo = new Usuario(2, "Arturo", "Cruz",
            nacimiento: new DateOnly(1999, 1, 1),
            pais: _pf.Argentina, @"hola@mimail.com");
    }
}