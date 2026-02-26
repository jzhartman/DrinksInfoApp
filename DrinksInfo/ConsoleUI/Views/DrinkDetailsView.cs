using DrinksInfo.Application.GetDrinkDetailsById;
using DrinksInfo.Application.GetDrinkImage;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

internal class DrinkDetailsView
{
    public void Render(DrinkDetailResponse drink, DrinkImageResponse imageData)
    {
        var table = CreateDrinkDetailsTable(drink);
        var canvasImage = GenerateCanvasImage(imageData);


        var columns = new Columns(
        [
            canvasImage,
            table
        ]);

        AnsiConsole.Write(columns);
    }

    private CanvasImage GenerateCanvasImage(DrinkImageResponse imageData)
    {
        using var stream = new MemoryStream(imageData.Bytes);
        var canvasImage = new CanvasImage(stream)
        {
            MaxWidth = 20
        };

        return canvasImage;
    }

    private Table CreateDrinkDetailsTable(DrinkDetailResponse drink)
    {
        var ingredientMeasurements = CombineIngredientsAndMeasurements(drink.Ingredients, drink.Measurements);

        var table = new Table()
                        .HideHeaders()
                        .NoBorder()
                        //.RoundedBorder()
                        //.BorderColor(Spectre.Console.Color.Blue)
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

        return table;
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
