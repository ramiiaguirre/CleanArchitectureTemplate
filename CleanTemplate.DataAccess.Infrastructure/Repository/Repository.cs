using CleanTemplate.Logic.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanTemplate.DataAccess.Infrastructure;

public class RepositoryEF<T> : IRepository<T> where T : class 
{
    private CleanTemplateContext _dbContext;
    private DbSet<T> _dbSet;

    public RepositoryEF(CleanTemplateContext context)
    {
        _dbContext = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> Add(T data)
    {
        _dbSet.Add(data);
        return data;
    }

    public async Task Delete(int id)
    {
        T? data = await _dbSet.FindAsync(id);

        if (data is not null)
            _dbSet.Remove(data);
    }

    public async Task<T?> Get(long id)
    {
        T? data = await _dbSet.FindAsync(id);
        return data;
    }

    public async Task<IEnumerable<T>> Get()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<T> Update(T data)
    {
        _dbSet.Attach(data);
        _dbContext.Entry(data).State = EntityState.Modified;
        return data;
    }
}