using System.Linq.Expressions;

namespace Fulbo12.Core.Persistencia.EFC.Repos;
public class RepoPersona : RepoGenerico<PersonaJuego>, IRepoPersona
{
    public RepoPersona(Fulbo12Contexto contexto) : base(contexto) { }
    public IEnumerable<PersonaJuego> BusquedaPersona(string? busqueda)
        => this.Obtener(filtro: FiltroBusqueda(busqueda));
    public async Task<IEnumerable<PersonaJuego>> BusquedaPersonaAsync(string? busqueda)
        => await this.ObtenerAsync(filtro: FiltroBusqueda(busqueda));
    private Expression<Func<PersonaJuego, bool>>? FiltroBusqueda(string? busqueda)
    {
        Expression<Func<PersonaJuego, bool>>? filtro = null;
        if (!string.IsNullOrEmpty(busqueda))
            filtro = ps => EF.Functions.IsMatch(new[] { ps.Nombre, ps.Apellido },
                                            busqueda,
                                            MySqlMatchSearchMode.Boolean
                                            );
        return filtro;
    }
}