using Fulbo12.Core.Futbol;

namespace Fulbo12.Core.Persistencia.EFC.Repos;
public class RepoFutbolista : RepoGenerico<Futbolista>, IRepoFutbolista
{
    public RepoFutbolista(Fulbo12Contexto contexto) : base(contexto) { }

    public async Task<Futbolista?> DetalleAsync(ushort id)
    {
        var futbolista = await DbSet.Where(f => f.Id == id)
                                .Include(f => f.Tipofutbolista)
                                .Include(f => f.Persona)
                                    .ThenInclude(p => p.Pais)
                                .Include(f => f.Equipo)
                                    .ThenInclude(e => e.Liga)
                                .FirstOrDefaultAsync()
                                .ConfigureAwait(false);
        if (futbolista is not null)
            await Contexto.Entry(futbolista)
                .Collection(f => f.Posiciones)
                .LoadAsync();

        return futbolista;
    }

    /*Puede ser tentador abstraer el código común, pero tener en cuenta que cambia
    la forma en que se selecciona elementos de la BD*/
    public bool ExisteFutbolistaCon(byte idPersona, byte idTipoFutbolista, byte idEquipo)
        => DbSet.Any(f => EF.Property<byte>(f, "idPersona") == idPersona
                    && EF.Property<byte>(f, "idEquipo") == idEquipo
                    && EF.Property<byte>(f, "idTipoFutbolista") == idTipoFutbolista);
    public async Task<bool> ExisteFutbolistaConAsync(byte idPersona, byte idTipoFutbolista, byte idEquipo)
        => await DbSet.AnyAsync(f => EF.Property<byte>(f, "idPersona") == idPersona
                    && EF.Property<byte>(f, "idEquipo") == idEquipo
                    && EF.Property<byte>(f, "idTipoFutbolista") == idTipoFutbolista);
}
