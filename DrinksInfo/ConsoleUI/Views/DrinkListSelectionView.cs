using DrinksInfo.Application.GetDrinksFromCategory;

namespace DrinksInfo.ConsoleUI.Views;

internal class DrinkListSelectionView
{
    public void Render(string categoryName, List<DrinkSummaryResponse> drinks)
    {
        Console.Clear();
        Console.WriteLine($"Drinks in {categoryName}:\r\n");
        int i = 1;
        foreach (var drink in drinks)
        {
            Console.WriteLine($"{drink.Id}\t{drink.Name}");
            i++;
        }
    }
}
