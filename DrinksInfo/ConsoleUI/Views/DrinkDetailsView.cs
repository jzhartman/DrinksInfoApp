using DrinksInfo.Application.GetDrinkDetailsById;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

internal class DrinkDetailsView
{
    public void Render(DrinkDetailResponse drink)
    {
        var ingredientMeasurements = CombineIngredientsAndMeasurements(drink.Ingredients, drink.Measurements);

        var table = new Table()
                        .HideHeaders()
                        .RoundedBorder()
                        .BorderColor(Color.Blue)
                        .ShowRowSeparators();

        table.AddColumn("Key");
        table.AddColumn("Value");

        table.AddRow("Id", $"{drink.Id}");
        table.AddRow("Drink", $"{drink.Name}");
        table.AddRow("Category", $"{drink.Category}");
        table.AddRow("Alcoholic", $"{drink.IsAlcoholic}");
        table.AddRow("Glass", $"{drink.Glass}");
        table.AddRow("Instructions", $"{drink.Insructions}");

        int i = 1;
        foreach (var item in ingredientMeasurements)
        {
            table.AddRow($"Ingredient {i}", $"{item}");
            i++;
        }

        AnsiConsole.Write(table);
    }

    private List<string> CombineIngredientsAndMeasurements(List<string> ingredients, List<string> measurements)
    {
        var output = new List<string>();
        var ingredientCount = ingredients.Count();
        var measurementsCount = measurements.Count();

        var maxLength = (ingredientCount >= measurementsCount) ? ingredientCount : measurementsCount;

        for (int i = 0; i < maxLength; i++)
        {
            string ingredient = string.Empty;
            string measurement = string.Empty;

            if (i < measurementsCount)
                measurement = (string.IsNullOrWhiteSpace(measurements[i])) ? "<BLANK>" : measurements[i];

            if (i < ingredientCount)
                ingredient = (string.IsNullOrWhiteSpace(ingredients[i])) ? "<BLANK>" : ingredients[i];

            output.Add($"{measurement} of {ingredient}");
        }

        return output;
    }
}
