using DrinksInfo.Application.GetCategories;

namespace DrinksInfo.ConsoleUI.Services;

internal class MainMenuService
{
    private readonly GetCategoriesHandler _getCategoriesHandler;

    public MainMenuService(GetCategoriesHandler getCategoriesHandler)
    {
        _getCategoriesHandler = getCategoriesHandler;
    }

    internal async void Run()
    {
        Console.WriteLine("Printing the list");

        var categories = await _getCategoriesHandler.Handle();

        foreach (var category in categories)
        {
            Console.WriteLine(category.Name);
        }
        Console.ReadLine();

        // Print main menu
        // 



        //using (var client = new HttpClient())
        //{
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));


        //    var result = await client.GetFromJsonAsync<GetCategoriesResponse>("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");
        //    var categories = result.Drinks;

        //    foreach (var category in categories)
        //    {
        //        Console.WriteLine(category.Name);
        //    }
        //}
    }
}

//internal record GetCategoriesResponse(List<Category> Drinks);
//internal record Category([property: JsonPropertyName("strCategory")] string Name);
