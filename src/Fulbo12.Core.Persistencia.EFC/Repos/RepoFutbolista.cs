using System.Data.Common;
using Fulbo12.Core.Futbol;
namespace Fulbo12.Core.Persistencia.EFC.Repos;

public class RepoFutbolista : RepoGenerico<Futbolista>, IRepoFutbolista
{
    public RepoFutbolista(Fulbo12Contexto contexto) : base(contexto) { }
    
    public bool ExisteFutbolistaCon(byte idPersona, byte idTipoFutbolista, byte idEquipo)
        => DbSet.Any(   f => EF.Property<byte>(f, "idPersona") == idPersona
                    &&  EF.Property<byte>(f, "idEquipo") == idEquipo
                    &&  EF.Property<byte>(f, "idTipoFutbolista") == idTipoFutbolista);
    public async Task<bool> ExisteFutbolistaConAsync(byte idPersona, byte idTipoFutbolista, byte idEquipo) 
        => await DbSet.AnyAsync(  f =>  EF.Property<byte>(f, "idPersona") == idPersona
                    &&  EF.Property<byte>(f, "idEquipo") == idEquipo
                    &&  EF.Property<byte>(f, "idTipoFutbolista") == idTipoFutbolista);
    /*public Futbolista? Detalle(ushort id )
    {
        var persona = DbSet.Include(f=>f.Persona)
                            .Include(f=>f.Equipo)
                            .Include(f=> f.Tipofutbolista);
        return(persona);
    }*/
}
