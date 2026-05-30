namespace ECommerce.Infrastructure;

using ECommerce.Application.Interfaces;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Infrastructure.Security;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Registrar DbContext con SQLite (Ciclo de vida Scoped por defecto)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // 2. Registrar los repositorios (SIEMPRE Scoped)
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>(); // El hasher puede ser Singleton porque no guarda estado
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}