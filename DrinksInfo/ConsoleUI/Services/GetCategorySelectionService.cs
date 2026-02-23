using DrinksInfo.Application.GetCategories;
using DrinksInfo.ConsoleUI.Views;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DrinksInfo.ConsoleUI.Services;

internal class GetCategorySelectionService
{
    private readonly CategoryListSelectionView _categorySelection;
    private readonly GetCategoriesHandler _getCategoriesHandler;

    public GetCategorySelectionService(CategoryListSelectionView categorySelection, GetCategoriesHandler getCategoriesHandler)
    {
        _categorySelection = categorySelection;
        _getCategoriesHandler = getCategoriesHandler;
    }

    public async Task Run()
    {
        while (true)
        {
            Console.Clear();
            var categories = await _getCategoriesHandler.Handle();
            //PrintCategories(categories);

            var selection = _categorySelection.Render(categories.ToArray());
            int selectionIndex = categories.FindIndex(category => category.Name == selection);

            //var selection = GetCategorySelectionFromUser(categories.Count());

            var drinks = await GetDrinksFromCategoryHandler(categories[selectionIndex].Name);
            PrintDrinkList(categories[selectionIndex].Name, drinks);

            Console.WriteLine("Press any key to continue or ESC to quit");

            var keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Escape) return;
        }
    }

    //private static int GetCategorySelectionFromUser(int max)
    //{
    //    bool validInput = false;
    //    int output = 0;

    //    while (validInput == false)
    //    {
    //        Console.Write("Enter ID of category to view: ");
    //        var input = Console.ReadLine();



    //        validInput = Int32.TryParse(input, out output);

    //        if (String.IsNullOrWhiteSpace(input)) Console.WriteLine("ERROR: Cannot be blank");
    //        else if (validInput == false) Console.WriteLine("ERROR: Input must be a number!");
    //        else if (validInput && (output <= 0 || output > max))
    //        {
    //            Console.WriteLine("Out of range");
    //            validInput = false;
    //        }
    //    }
    //    return output;
    //}

    static async Task<List<DrinkSummary>> GetDrinksFromCategoryHandler(string categoryName)
    {
        string filter = categoryName.Replace(" ", "_");

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var result = await client.GetFromJsonAsync<GetDrinkListResponse>($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={categoryName}");

        return result.Drinks;
    }
    //static void PrintCategories(List<GetCategoriesResponse> categories)
    //{
    //    Console.Clear();
    //    int i = 1;
    //    foreach (var category in categories)
    //    {
    //        Console.WriteLine($"{i}:\t{category.Name}");
    //        i++;
    //    }
    //}

    static void PrintDrinkList(string categoryName, List<DrinkSummary> drinks)
    {
        Console.Clear();
        Console.WriteLine($"Drinks in {categoryName}:\r\n");
        int i = 1;
        foreach (var drink in drinks)
        {
            Console.WriteLine($"{drink.idDrink}\t{drink.strDrink}");
            i++;
        }
    }
}

internal record GetDrinkListResponse(List<DrinkSummary> Drinks);
internal record DrinkSummary(string strDrink, int idDrink);
