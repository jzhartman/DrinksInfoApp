using DrinksInfo.Application.GetDrinkImage;
using Spectre.Console;

namespace DrinksInfo.ConsoleUI.Views;

public class DrinkImageView
{
    public void Render(DrinkImageResponse imageData)
    {
        using var stream = new MemoryStream(imageData.Bytes);
        var canvasImage = new CanvasImage(stream)
        {
            MaxWidth = 100
        };

        AnsiConsole.Write(canvasImage);
    }
}
