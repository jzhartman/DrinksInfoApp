using DrinksInfo.ConsoleUI.Input;
using DrinksInfo.ConsoleUI.Output;
using DrinksInfo.ConsoleUI.Services;
using DrinksInfo.ConsoleUI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.ConsoleUI;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<CategoryListService>();
        services.AddTransient<DrinkDetailService>();

        services.AddTransient<CategoryListSelectionView>();
        services.AddTransient<DrinkListSelectionView>();
        services.AddTransient<DrinkDetailsView>();
        services.AddTransient<DrinkImageView>();

        services.AddTransient<ConsoleOutput>();
        services.AddTransient<UserInput>();

        return services;
    }
}
