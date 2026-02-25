using DrinksInfo.ConsoleUI.Services;
using DrinksInfo.ConsoleUI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.ConsoleUI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<ProcessDrinkSelection>();
        services.AddScoped<CategoryListSelectionView>();
        services.AddScoped<DrinkListSelectionView>();
        services.AddScoped<DrinkDetailsView>();

        return services;
    }
}
