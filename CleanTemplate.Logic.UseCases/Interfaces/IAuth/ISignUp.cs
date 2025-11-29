using CleanTemplate.Model.Domain;

public interface ISignUp
{
    Task<User> Execute(User user);
}