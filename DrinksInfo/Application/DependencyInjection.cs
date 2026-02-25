using DrinksInfo.Application.GetCategories;
using DrinksInfo.Application.GetDrinkDetailsById;
using DrinksInfo.Application.GetDrinksFromCategory;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetCategoriesHandler>();
        services.AddScoped<GetDrinksSummaryByCategoryNameHandler>();
        services.AddScoped<GetDrinkDetailsByIdHandler>();

        return services;
    }
}
