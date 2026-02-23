using DrinksInfo.Application.GetCategories;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

public class CategoryListSelectionView
{
    public string Render(GetCategoriesResponse[] categories)
    {
        var selection = AnsiConsole.Prompt(
                            new SelectionPrompt<GetCategoriesResponse>()
                            .Title("Select a category from below: ")
                            .PageSize(15)
                            .UseConverter(category => $"{category.Name}")
                            .AddChoices(categories));

        return selection.Name;
    }
}
