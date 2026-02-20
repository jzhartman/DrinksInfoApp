using DrinksInfo.ConsoleUI.Services;
using DrinksInfo.ConsoleUI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.ConsoleUI;

public static class DependencyInjection
{
    public static IServiceCollection AddConsoleUI(this IServiceCollection services)
    {
        services.AddScoped<MainMenuService>();
        services.AddScoped<CategoryListView>();

        return services;
    }
}
