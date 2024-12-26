using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RolePolicy.Domain.Interfaces;
using RolePolicy.Persistence.Repository;

namespace RolePolicy.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection:"];
        services.AddDbContext<RolePolicyDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IActionRepository, ActionRepository>();
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<IRoleAccessRepository, RoleAccessRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRoleResourceActionRepository, RoleResourceActionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
