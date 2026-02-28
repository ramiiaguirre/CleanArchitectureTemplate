using CleanTemplate.Logic.UseCases;
using CleanTemplate.Logic.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

public static class AuthDependencyInjection
{
    public static IServiceCollection AddAuthUseCases(this IServiceCollection services)
    {
        services.AddScoped<ISignUp, SignUp>();
        services.AddScoped<ILogIn, LogIn>();        
        return services;
    }
}   