using CleanTemplate.Model.Domain;

namespace CleanTemplate.Model.Domain.Interfaces;
public interface IUnitOfWork
{
    public IRepository<User> Users { get; }

    public Task Save();
}