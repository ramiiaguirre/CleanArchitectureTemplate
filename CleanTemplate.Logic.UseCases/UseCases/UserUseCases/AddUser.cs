using CleanTemplate.Logic.Repository;
using CleanTemplate.Model.Domain;

public class AddUser
{
    private readonly IRepository<User> _repository = default!;
    public AddUser(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task Execute(User user)
    {
        await _repository.Add(user);
    }
}