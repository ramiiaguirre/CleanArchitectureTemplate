using CleanTemplate.Logic.Repository;
using CleanTemplate.Model.Domain;

public class LogIn
{
    private readonly IRepository<User> _repository = default!;
    public LogIn(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<User?> Execute(string name, string passwordHash)
    {
        var loggedInUser = await _repository.Get(
            u => u.Name == name && u.Password == passwordHash, 
            u => u.Roles!
        );
        return loggedInUser;
    }
    
}