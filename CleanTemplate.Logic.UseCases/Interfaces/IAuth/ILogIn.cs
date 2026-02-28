using CleanTemplate.Logic.UseCases.DTOs;

namespace CleanTemplate.Logic.UseCases.Interfaces;
public interface ILogIn
{
    Task<UserDTO?> Execute(LoginDTO login);
}