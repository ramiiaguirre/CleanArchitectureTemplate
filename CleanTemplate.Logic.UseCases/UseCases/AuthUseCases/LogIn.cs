using CleanTemplate.Logic.UseCases.Interfaces;
using CleanTemplate.Logic.UseCases.DTOs;
using CleanTemplate.Model.Domain;
using CleanTemplate.Model.Domain.Interfaces;

namespace CleanTemplate.Logic.UseCases;

public class LogIn : ILogIn
{
    private readonly IRepository<User> _repository = default!;
    public LogIn(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<UserDTO?> Execute(LoginDTO login)
    {
        var loggedInUser = await _repository.Get(
            u => u.Name == login.Name && u.Password == login.Password, 
            u => u.Roles!
        );
        return new UserDTO(); //Do Mapping
    }
    
}