using DrinksInfo.Application.GetCategories;
using DrinksInfo.Application.GetDrinkDetailsById;
using DrinksInfo.Application.GetDrinkImage;
using DrinksInfo.Application.GetDrinksFromCategory;
using DrinksInfo.ConsoleUI.Views;

namespace DrinksInfo.ConsoleUI.Services;

internal class ProcessDrinkSelection
{
    private readonly CategoryListSelectionView _categorySelection;
    private readonly DrinkListSelectionView _drinkListSelection;
    private readonly DrinkDetailsView _drinkDetails;
    private readonly GetCategoriesHandler _getCategoriesHandler;
    private readonly GetDrinksSummaryByCategoryNameHandler _getDrinksHandler;
    private readonly GetDrinkDetailsByIdHandler _getDrinkDetailsHandler;
    private readonly GetDrinkImageHandler _getDrinkImageHandler;

    public ProcessDrinkSelection(CategoryListSelectionView categorySelection, DrinkListSelectionView drinkListSelection, DrinkDetailsView drinkDetails,
                                        GetCategoriesHandler getCategoriesHandler, GetDrinksSummaryByCategoryNameHandler getDrinksHandler,
                                        GetDrinkDetailsByIdHandler getDrinkDetailsHandler, GetDrinkImageHandler getDrinkImageHandler)
    {
        _categorySelection = categorySelection;
        _drinkListSelection = drinkListSelection;
        _drinkDetails = drinkDetails;
        _getCategoriesHandler = getCategoriesHandler;
        _getDrinksHandler = getDrinksHandler;
        _getDrinkDetailsHandler = getDrinkDetailsHandler;
        _getDrinkImageHandler = getDrinkImageHandler;
    }

    public async Task Run()
    {
        var keyInfo = new ConsoleKeyInfo();

        while (true)
        {
            Console.Clear();
            var categories = await _getCategoriesHandler.HandleAsync();

            while (true)
            {
                var categorySelection = _categorySelection.Render(categories.ToArray());
                int selectionIndex = categories.FindIndex(category => category.Name == categorySelection);

                var drinks = await _getDrinksHandler.Handle(categories[selectionIndex].Name);

                while (true)
                {
                    var drinkSelection = _drinkListSelection.Render(categories[selectionIndex].Name, drinks);

                    var drink = await _getDrinkDetailsHandler.HandleAsync(drinkSelection);
                    Console.Clear();
                    var imageData = await _getDrinkImageHandler.Handle(drink.ImageUrl);

                    _drinkDetails.Render(drink, imageData);

                    Console.WriteLine($"Press any key to return to {drink.Category} List or ESC to return to Category Selection");

                    keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Escape) break;
                }

                Console.Clear();
                Console.WriteLine($"Press any key to return to Category Selection or ESC to exit app");

                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape) break;
            }
        }
    }
}
