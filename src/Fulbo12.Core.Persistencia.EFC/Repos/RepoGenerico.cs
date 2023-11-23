using System.Linq.Expressions;

namespace Fulbo12.Core.Persistencia.EFC.Repos;
public abstract class RepoGenerico<T> : IRepo<T> where T : class
{
    protected readonly Fulbo12Contexto Contexto;
    protected DbSet<T> DbSet => Contexto.Set<T>();
    public RepoGenerico(Fulbo12Contexto contexto) => Contexto = contexto;
    public void Alta(T entidad) => DbSet.Add(entidad);
    public void Alta(IEnumerable<T> entidades) => DbSet.AddRange(entidades);
    public async Task AltaAsync(T entidad)
        => await DbSet.AddAsync(entidad);
    private IQueryable<T> ConfigurarConsulta(Expression<Func<T, bool>>? filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orden = null, string? includes = null)
    {
        IQueryable<T> query = DbSet;

        if (filtro != null)
            query = query.Where(filtro);

        if (includes != null)
            foreach (var propiedad in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(propiedad);
            }
        
        if (orden != null)
            query = orden(query);
        
        return query;
    }
    public virtual IEnumerable<T> Obtener(  Expression<Func<T, bool>>? filtro = null,
                                            Func<IQueryable<T>, IOrderedQueryable<T>>? orden = null,
                                            string? includes = null)
        => ConfigurarConsulta(filtro, orden, includes)
                .ToList();
    public virtual async Task<IEnumerable<T>> ObtenerAsync( Expression<Func<T, bool>>? filtro = null,
                                                            Func<IQueryable<T>, IOrderedQueryable<T>>? orden = null,
                                                            string? includes = null)
        => await ConfigurarConsulta(filtro, orden, includes)
                .ToListAsync();
    public T? ObtenerPorId(params object[] claves) => DbSet.Find(claves);

    public async Task<T?> ObtenerPorIdAsync(params object[] claves)
        => await DbSet.FindAsync(claves);

    public void Modificar(T entidad) => DbSet.Update(entidad);
}