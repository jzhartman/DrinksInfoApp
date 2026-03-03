using DrinksInfo.Application.GetCategories;
using DrinksInfo.Application.GetDrinkDetailsById;
using DrinksInfo.Application.GetDrinkImage;
using DrinksInfo.Application.GetDrinksFromCategory;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<GetCategoriesHandler>();
        services.AddTransient<GetDrinksSummaryByCategoryNameHandler>();
        services.AddTransient<GetDrinkDetailsByIdHandler>();
        services.AddTransient<GetDrinkImageHandler>();

        return services;
    }
}
