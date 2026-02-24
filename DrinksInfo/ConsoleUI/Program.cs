using DrinksInfo.Application;
using DrinksInfo.ConsoleUI.Services;
using DrinksInfo.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.ConsoleUI;

internal class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddInfrastructure();
        services.AddPresentation();
        services.AddApplication();

        var provider = services.BuildServiceProvider();

        var categorySelection = provider.GetRequiredService<GetCategorySelectionService>();

        await categorySelection.Run();
    }
}