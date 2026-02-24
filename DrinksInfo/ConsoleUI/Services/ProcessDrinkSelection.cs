using DrinksInfo.Application.GetCategories;
using DrinksInfo.Application.GetDrinksFromCategory;
using DrinksInfo.ConsoleUI.Views;

namespace DrinksInfo.ConsoleUI.Services;

internal class ProcessDrinkSelection
{
    private readonly CategoryListSelectionView _categorySelection;
    private readonly DrinkListSelectionView _drinkListSelection;
    private readonly GetCategoriesHandler _getCategoriesHandler;
    private readonly GetDrinksSummaryByCategoryNameHandler _getDrinksHandler;

    public ProcessDrinkSelection(CategoryListSelectionView categorySelection, DrinkListSelectionView drinkListSelection,
                                        GetCategoriesHandler getCategoriesHandler, GetDrinksSummaryByCategoryNameHandler getDrinksHandler)
    {
        _categorySelection = categorySelection;
        _drinkListSelection = drinkListSelection;
        _getCategoriesHandler = getCategoriesHandler;
        _getDrinksHandler = getDrinksHandler;
    }

    public async Task Run()
    {
        while (true)
        {
            Console.Clear();
            var categories = await _getCategoriesHandler.Handle();
            var categorySelection = _categorySelection.Render(categories.ToArray());

            int selectionIndex = categories.FindIndex(category => category.Name == categorySelection);

            var drinks = await _getDrinksHandler.Handle(categories[selectionIndex].Name);
            var drinkSelection = _drinkListSelection.Render(categories[selectionIndex].Name, drinks);

            Console.WriteLine($"Selected drink Id: {drinkSelection}");

            Console.WriteLine("Press any key to continue or ESC to quit");

            var keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Escape) return;
        }
    }
}
