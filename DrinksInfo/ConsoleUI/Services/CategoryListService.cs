using DrinksInfo.Application.GetCategories;
using DrinksInfo.Application.GetDrinksFromCategory;
using DrinksInfo.ConsoleUI.Enums;
using DrinksInfo.ConsoleUI.Helpers;
using DrinksInfo.ConsoleUI.Input;
using DrinksInfo.ConsoleUI.Output;
using DrinksInfo.ConsoleUI.Views;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.ConsoleUI.Services;

public class CategoryListService
{
    private readonly CategoryListSelectionView _categorySelection;
    private readonly DrinkListSelectionView _drinkListSelection;
    private readonly DrinkDetailService _drinkDetailService;
    private readonly GetCategoryListHandler _getCategoryListHandler;
    private readonly GetDrinksSummaryByCategoryNameHandler _getDrinksHandler;
    private readonly ConsoleOutput _output;
    private readonly UserInput _input;

    public CategoryListService(CategoryListSelectionView categorySelection, DrinkListSelectionView drinkListSelection,
                                GetCategoryListHandler getCategoryListHandler, GetDrinksSummaryByCategoryNameHandler getDrinksHandler,
                                DrinkDetailService drinkDetailService, ConsoleOutput output, UserInput input)
    {
        _categorySelection = categorySelection;
        _drinkListSelection = drinkListSelection;
        _getCategoryListHandler = getCategoryListHandler;
        _getDrinksHandler = getDrinksHandler;
        _drinkDetailService = drinkDetailService;
        _output = output;
        _input = input;
    }

    public async Task RunAsync()
    {
        Console.Clear();
        var categoryListResult = await ConsoleStatusHelper.StatusAsync("Fetching category list...", () =>
                                            _getCategoryListHandler.HandleAsync());

        if (categoryListResult.IsSuccess && categoryListResult.Value != null)
            await ManageCategorySelection(categoryListResult.Value);
        else
            ManageErrorPrinting(categoryListResult.Errors);
    }

    private async Task ManageCategorySelection(List<CategoryResponse> category)
    {
        var exitCode = ExitCode.None;
        bool returnToMainMenu = false;

        while (returnToMainMenu == false)
        {
            Console.Clear();
            var categorySelection = _categorySelection.Render(category.ToArray());
            int selectionIndex = category.FindIndex(category => category.Name == categorySelection);


            var drinksResult = await ConsoleStatusHelper.StatusAsync($"Fetching drinks in {categorySelection}...", () =>
                                            _getDrinksHandler.HandleAsync(category[selectionIndex].Name));

            if (drinksResult.IsSuccess && drinksResult.Value != null)
                exitCode = await ManageDrinkDetails(category[selectionIndex].Name, drinksResult.Value);
            else
                ManageErrorPrinting(drinksResult.Errors);

            if (exitCode == ExitCode.MainMenu)
                returnToMainMenu = true;
            else
                returnToMainMenu = false;
        }
    }

    private async Task<ExitCode> ManageDrinkDetails(string categoryName, List<DrinkSummaryResponse> drinks)
    {
        var exitCode = ExitCode.None;
        bool returnToCategoryMenu = false;

        while (returnToCategoryMenu == false)
        {
            var drinkSelection = _drinkListSelection.Render(categoryName, drinks);

            exitCode = await _drinkDetailService.ManageDrinkDetailsAsync(drinkSelection);

            if (exitCode == ExitCode.CategorySelection || exitCode == ExitCode.MainMenu)
                returnToCategoryMenu = true;
            else
                returnToCategoryMenu = false;
        }
        return exitCode;
    }

    private void ManageErrorPrinting(IEnumerable<Error> errors)
    {
        _output.OutputErrorMessage(errors);
        _input.PressAnyKeyToContinue();
    }
}
