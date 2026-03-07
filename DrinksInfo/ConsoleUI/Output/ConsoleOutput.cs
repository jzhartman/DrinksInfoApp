using DrinksInfo.Domain.Validation;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Output;

public class ConsoleOutput
{
    public void RenderDrinkDetailKeyOptions()
    {
        var table = new Table()
                        .RoundedBorder()
                        .BorderColor(Spectre.Console.Color.Blue)
                        .ShowRowSeparators();

        table.AddColumn("Key");
        table.AddColumn("Operation");

        table.AddRow("V", "View Drink Image");
        table.AddRow("F", "Add Drink to Favorites");
        table.AddRow("X", "Delete Drink from Favorites");
        table.AddRow("D", "Return to Drink Selection");
        table.AddRow("C", "Return to Category Selection");
        table.AddRow("M", "Return to Main Menu");

        AnsiConsole.Write(table);
    }

    public void InputErrorMessage()
    {
        AnsiConsole.MarkupLine("[red]ERROR:[/] Invalid key press! Please enter one of the displayed options.");
    }

    public void OutputErrorMessage(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            AnsiConsole.MarkupLine($"[red]ERROR:[/] {error.Description}");
        }
    }
    public void PrintAddFavoriteSuccessMessage(string name)
    {
        AnsiConsole.MarkupLine($"[green]SUCCESS:[/] Added {name} to favorites list!");
    }
    public void PrintDeleteFavoriteSuccessMessage(string name)
    {
        AnsiConsole.MarkupLine($"[green]SUCCESS:[/] Deleted {name} from favorites list!");
    }
}
