using CleanTemplate.Model.Domain;

namespace CleanTemplate.Logic.UseCases.Repository;
public interface IUnitOfWork
{
    public IRepository<User> Users { get; }

    public Task Save();
}