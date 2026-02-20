using DrinksInfo.Application.GetCategories;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.Application;

internal static class DependencyInjection
{
    internal static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetCategoriesHandler>();

        return services;
    }
}
