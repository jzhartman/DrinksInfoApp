using DrinksInfo.Application.GetCategories;
using DrinksInfo.Application.GetDrinksFromCategory;
using DrinksInfo.ConsoleUI.Views;

namespace DrinksInfo.ConsoleUI.Services;

internal class CategoryListService
{
    private readonly CategoryListSelectionView _categorySelection;
    private readonly DrinkListSelectionView _drinkListSelection;
    private readonly DrinkDetailService _drinkDetailService;
    private readonly GetCategoriesHandler _getCategoriesHandler;
    private readonly GetDrinksSummaryByCategoryNameHandler _getDrinksHandler;

    public CategoryListService(CategoryListSelectionView categorySelection, DrinkListSelectionView drinkListSelection,
                                GetCategoriesHandler getCategoriesHandler, GetDrinksSummaryByCategoryNameHandler getDrinksHandler,
                                DrinkDetailService drinkDetailService
                                         )
    {
        _categorySelection = categorySelection;
        _drinkListSelection = drinkListSelection;
        _getCategoriesHandler = getCategoriesHandler;
        _getDrinksHandler = getDrinksHandler;
        _drinkDetailService = drinkDetailService;
    }

    public async Task Run()
    {
        while (true)
        {
            var categories = await _getCategoriesHandler.HandleAsync();

            while (true)
            {
                Console.Clear();
                var categorySelection = _categorySelection.Render(categories.ToArray());
                int selectionIndex = categories.FindIndex(category => category.Name == categorySelection);

                var drinks = await _getDrinksHandler.Handle(categories[selectionIndex].Name);

                bool returnToCategoryMenu = false;

                while (returnToCategoryMenu == false)
                {
                    var drinkSelection = _drinkListSelection.Render(categories[selectionIndex].Name, drinks);

                    returnToCategoryMenu = await _drinkDetailService.ManageDrinkDetails(drinkSelection);
                }
            }
        }
    }
}
