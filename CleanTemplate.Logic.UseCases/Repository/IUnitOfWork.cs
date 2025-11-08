using CleanTemplate.Model.Domain;

namespace CleanTemplate.Logic.Repository;
public interface IUnitOfWork
{
    public IRepository<User> Users { get; }

    public Task Save();
}