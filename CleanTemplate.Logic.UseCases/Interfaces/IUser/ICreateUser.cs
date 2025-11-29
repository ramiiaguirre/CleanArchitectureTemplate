using CleanTemplate.Model.Domain;

public interface ICreateUser
{
    public Task Execute(User user);
}