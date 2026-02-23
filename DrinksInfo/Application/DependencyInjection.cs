using DrinksInfo.Application.GetCategories;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetCategoriesHandler>();

        return services;
    }
}
