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
        table.AddRow("D", "Return to Drink Selection");
        table.AddRow("C", "Return to Category Selection");

        AnsiConsole.Write(table);
    }

    public void InputErrorMessage()
    {
        AnsiConsole.MarkupLine("[red]ERROR:[/] Invalid key press! Please enter one of the displayed options.");
    }
}
