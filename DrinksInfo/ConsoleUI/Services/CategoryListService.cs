using DrinksInfo.Application.GetCategories;
using DrinksInfo.Application.GetDrinksFromCategory;
using DrinksInfo.ConsoleUI.Input;
using DrinksInfo.ConsoleUI.Output;
using DrinksInfo.ConsoleUI.Views;

namespace DrinksInfo.ConsoleUI.Services;

internal class CategoryListService
{
    private readonly CategoryListSelectionView _categorySelection;
    private readonly DrinkListSelectionView _drinkListSelection;
    private readonly DrinkDetailService _drinkDetailService;
    private readonly GetCategoriesHandler _getCategoriesHandler;
    private readonly GetDrinksSummaryByCategoryNameHandler _getDrinksHandler;
    private readonly ConsoleOutput _output;
    private readonly UserInput _input;

    public CategoryListService(CategoryListSelectionView categorySelection, DrinkListSelectionView drinkListSelection,
                                GetCategoriesHandler getCategoriesHandler, GetDrinksSummaryByCategoryNameHandler getDrinksHandler,
                                DrinkDetailService drinkDetailService, ConsoleOutput output, UserInput input)
    {
        _categorySelection = categorySelection;
        _drinkListSelection = drinkListSelection;
        _getCategoriesHandler = getCategoriesHandler;
        _getDrinksHandler = getDrinksHandler;
        _drinkDetailService = drinkDetailService;
        _output = output;
        _input = input;
    }

    public async Task Run()
    {
        while (true)
        {
            Console.Clear();
            var categoriesResult = await _getCategoriesHandler.HandleAsync();

            if (categoriesResult.IsSuccess && categoriesResult.Value != null)
            {
                while (true)
                {
                    Console.Clear();
                    var categorySelection = _categorySelection.Render(categoriesResult.Value.ToArray());
                    int selectionIndex = categoriesResult.Value.FindIndex(category => category.Name == categorySelection);

                    var drinks = await _getDrinksHandler.HandleAsync(categoriesResult.Value[selectionIndex].Name);

                    bool returnToCategoryMenu = false;

                    while (returnToCategoryMenu == false)
                    {
                        var drinkSelection = _drinkListSelection.Render(categoriesResult.Value[selectionIndex].Name, drinks);

                        returnToCategoryMenu = await _drinkDetailService.ManageDrinkDetailsAsync(drinkSelection);
                    }
                }
            }
            else
            {
                _output.OutputErrorMessage(categoriesResult.Errors);
                _input.PressAnyKeyToContinue();
            }

        }
    }
}
