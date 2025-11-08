namespace CleanTemplate.Logic.Repository;

public interface IRepository<T>
{
    Task<T?> Get(long id);
    Task<IEnumerable<T>> Get();
    Task<T> Add(T data);
    Task Delete(int id);
    Task<T> Update(T data);

    Task Save();
}