using DrinksInfo.Application.GetDrinkDetailsById;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

public class DrinkDetailsView
{
    public void Render(DrinkDetailResponse drink)
    {
        AnsiConsole.MarkupInterpolated($"[bold blue]Drink:[/] [bold underline green]{drink.Name}[/]");

        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        var descriptionTable = GenerateDescriptionTable(drink);
        AnsiConsole.Write(descriptionTable);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold blue]Instructions:[/]");
        AnsiConsole.WriteLine($"{drink.Insructions}");

        var ingredientMeasurements = CombineIngredientsAndMeasurements(drink.Ingredients, drink.Measurements);


        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold blue]Ingredients:[/]");
        GenerateIngredientsList(ingredientMeasurements);
    }

    private Table GenerateDescriptionTable(DrinkDetailResponse drink)
    {
        var table = new Table()
                .HideHeaders()
                .NoBorder();

        table.AddColumn("Key");
        table.AddColumn("Value");
        table.AddColumn("Spacing");
        table.AddColumn("Key");
        table.AddColumn("Value");

        table.AddRow("[blue]Id:[/]", $"{drink.Id}", "      ", "[blue]Category:[/]", $"{drink.Category}");
        table.AddRow("[blue]Glass:[/]", $"{drink.Glass}", "      ", "[blue]Alcoholic:[/]", $"{drink.IsAlcoholic}");

        return table;
    }

    private void GenerateIngredientsList(List<string> ingredients)
    {
        foreach (var item in ingredients)
        {
            AnsiConsole.WriteLine($"{item}");
        }
    }

    private List<string> CombineIngredientsAndMeasurements(List<string> ingredients, List<string> measurements)
    {
        var output = new List<string>();
        var ingredientCount = ingredients.Count();
        var measurementsCount = measurements.Count();

        var maxLength = (ingredientCount >= measurementsCount) ? ingredientCount : measurementsCount;

        for (int i = 0; i < maxLength; i++)
        {
            string ingredient = ingredients[i].Trim().Replace("\n", ""); ;
            string measurement = measurements[i].Trim().Replace("\n", ""); ;

            if (measurement != "<empty>" && ingredient != "<empty>")
                output.Add($"{measurement} of {ingredient}");

            if (measurement == "<empty>" && ingredient != "<empty>")
                output.Add($"{ingredient}");

            if (measurement != "<empty>" && ingredient == "<empty>")
                output.Add($"{measurement} of <UNKNOWN>");
        }

        return output;
    }
}
