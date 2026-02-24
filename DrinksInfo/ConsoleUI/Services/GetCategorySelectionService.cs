using DrinksInfo.Application.GetCategories;
using DrinksInfo.Application.GetDrinksFromCategory;
using DrinksInfo.ConsoleUI.Views;

namespace DrinksInfo.ConsoleUI.Services;

internal class GetCategorySelectionService
{
    private readonly CategoryListSelectionView _categorySelection;
    private readonly DrinkListSelectionView _drinkListSelection;
    private readonly GetCategoriesHandler _getCategoriesHandler;
    private readonly GetDrinksSummaryByCategoryNameHandler _getDrinksHandler;

    public GetCategorySelectionService(CategoryListSelectionView categorySelection, DrinkListSelectionView drinkListSelection,
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

            var selection = _categorySelection.Render(categories.ToArray());
            int selectionIndex = categories.FindIndex(category => category.Name == selection);


            var drinks = await _getDrinksHandler.Handle(categories[selectionIndex].Name);
            _drinkListSelection.Render(categories[selectionIndex].Name, drinks);

            Console.WriteLine("Press any key to continue or ESC to quit");

            var keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Escape) return;
        }
    }

}
