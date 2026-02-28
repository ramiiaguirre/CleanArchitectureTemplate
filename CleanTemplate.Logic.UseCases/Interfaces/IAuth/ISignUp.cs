using CleanTemplate.Logic.UseCases.DTOs;
namespace CleanTemplate.Logic.UseCases.Interfaces;
public interface ISignUp
{
    Task<UserDTO> Execute(SignUpDTO signUp);
}