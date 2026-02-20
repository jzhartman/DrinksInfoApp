using DrinksInfo.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<Repository>();
        services.AddHttpClient<IRepository, Repository>();

        return services;
    }
}
