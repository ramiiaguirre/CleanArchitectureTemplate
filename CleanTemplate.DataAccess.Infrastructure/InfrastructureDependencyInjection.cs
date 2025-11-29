using CleanTemplate.Logic.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTemplate.DataAccess.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<CleanTemplateContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        // Repositorios
        services.AddScoped(typeof(IRepository<>), typeof(RepositoryEF<>));
        
        return services;
    }
}