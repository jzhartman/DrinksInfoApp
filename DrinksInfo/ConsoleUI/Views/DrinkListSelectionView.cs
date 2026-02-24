using DrinksInfo.Application.GetDrinksFromCategory;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

internal class DrinkListSelectionView
{

    public int Render(string categoryName, List<DrinkSummaryResponse> drinks)
    {
        Console.Clear();
        var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<DrinkSummaryResponse>()
                    .Title($"Select a drink from the {categoryName} list:")
                    .PageSize(15)
                    .EnableSearch()
                    .SearchPlaceholderText("Begin typing to search drink list...")
                    .UseConverter(d => $"{d.Name}")
                    .AddChoices(drinks));

        return selection.Id;
    }
}
