namespace Server.Shared.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(Guid id);
    Task<bool> Create(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(T entity);
    Task<List<T>> List();
}
