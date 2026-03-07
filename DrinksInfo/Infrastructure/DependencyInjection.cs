using DrinksInfo.Application.Interfaces;
using DrinksInfo.Infrastructure.Interfaces;
using DrinksInfo.Infrastructure.Repositories;
using DrinksInfo.Infrastructure.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddTransient<ISqliteConnectionFactory, SqliteConnectionFactory>(provider => new SqliteConnectionFactory(connectionString));
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

        services.AddTransient<IDrinkRepository, DrinkRepository>();
        services.AddTransient<IFavoriteDrinkRepository, FavoriteDrinkRepository>();

        return services;
    }
}
