using DrinksInfo.Application.GetCategories;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

public class CategoryListSelectionView
{
    public string Render(CategoryListResponse[] categories)
    {
        var selection = AnsiConsole.Prompt(
                            new SelectionPrompt<CategoryListResponse>()
                            .Title("Select a category from below: ")
                            .PageSize(15)
                            .WrapAround()
                            .UseConverter(category => $"{category.Name}")
                            .AddChoices(categories));

        return selection.Name;
    }
}
