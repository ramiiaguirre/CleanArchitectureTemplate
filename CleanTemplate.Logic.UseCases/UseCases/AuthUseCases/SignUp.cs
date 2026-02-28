using CleanTemplate.Logic.UseCases.DTOs;
using CleanTemplate.Logic.UseCases.Interfaces;
using CleanTemplate.Model.Domain;
using CleanTemplate.Model.Domain.Interfaces;

namespace CleanTemplate.Logic.UseCases;

public class SignUp : ISignUp
{
    private readonly IRepository<User> _repository = default!;
    public SignUp(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<UserDTO> Execute(SignUpDTO signUp)
    {
        // Validate signUp
         var user = new User();
        // user.Validate(signUp.Name, signUp.password, signUp.Age);

        var userCreated = await _repository.Add(user);
        await _repository.Save();
        return new UserDTO
        {
            Name = userCreated.Name,
            Roles = userCreated.Roles?.Select(r => r.Name).ToList()
        };
    }
    
}