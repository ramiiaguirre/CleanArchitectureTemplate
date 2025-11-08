using CleanTemplate.Logic.Repository;
using CleanTemplate.Model.Domain;

public class GetUserById
{
    private readonly IRepository<User> _repository = default!;
    public GetUserById(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<User?> Execute(long id)
    {
        return await _repository.Get(id);
    }
}