namespace Server.Application.Interfaces.Repository;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(Guid id);
    Task<bool> Create(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(T entity);
    Task<List<T>> List();
}
