using Fulbo12.Core.Futbol;
namespace Fulbo12.Core.Persistencia.Repos;
public interface IRepoFutbolista : IRepo<Futbolista> 
{ 
    public bool ExisteFutbolistaCon(byte idPersona, byte idTipoFutbolista, byte idEquipo);

    public Task<bool> ExisteFutbolistaConAsync(byte idPersona, byte idTipoFutbolista, byte idEquipo); 
    public Task<Futbolista?> DetalleAsync(ushort id);
}