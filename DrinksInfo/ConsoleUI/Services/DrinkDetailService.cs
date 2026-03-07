using DrinksInfo.Application.DrinkInfoApi.GetDrinkDetailsById;
using DrinksInfo.Application.DrinkInfoApi.GetDrinkImage;
using DrinksInfo.Application.DrinkInfoApi.GetDrinksSummaryByCategoryName;
using DrinksInfo.Application.Favorites.AddFavoriteDrink;
using DrinksInfo.ConsoleUI.Enums;
using DrinksInfo.ConsoleUI.Helpers;
using DrinksInfo.ConsoleUI.Input;
using DrinksInfo.ConsoleUI.Output;
using DrinksInfo.ConsoleUI.Views;

namespace DrinksInfo.ConsoleUI.Services;

public class DrinkDetailService
{
    private readonly GetDrinkDetailsByIdHandler _getDrinkDetailsHandler;
    private readonly GetDrinkImageHandler _getDrinkImageHandler;
    private readonly AddFavoriteDrinkHandler _addFavoriteHandler;

    private readonly DrinkDetailsView _drinkDetails;
    private readonly DrinkImageView _drinkImage;
    private readonly ConsoleOutput _output;
    private readonly UserInput _input;


    public DrinkDetailService(GetDrinkDetailsByIdHandler getDrinkDetailsHandler, GetDrinkImageHandler getDrinkImageHandler,
                                AddFavoriteDrinkHandler addFavoriteHandler,
                                DrinkDetailsView drinkDetails, DrinkImageView drinkImage, ConsoleOutput output, UserInput input)
    {
        _getDrinkDetailsHandler = getDrinkDetailsHandler;
        _getDrinkImageHandler = getDrinkImageHandler;
        _addFavoriteHandler = addFavoriteHandler;
        _drinkDetails = drinkDetails;
        _drinkImage = drinkImage;
        _output = output;
        _input = input;
    }

    public async Task<ExitCode> ManageDrinkDetailsAsync(DrinkSummaryResponse drinkSelection)
    {
        var exitCode = ExitCode.None;
        bool returnToDrinkSelection = false;

        while (returnToDrinkSelection == false)
        {
            Console.Clear();
            var drinkDetailResult = await ConsoleStatusHelper.ShowStatusAsync($"Fetching {drinkSelection.Name} details...", () =>
                                            _getDrinkDetailsHandler.HandleAsync(drinkSelection.Id));

            if (drinkDetailResult.IsSuccess && drinkDetailResult.Value != null)
            {
                _drinkDetails.Render(drinkDetailResult.Value);

                Console.WriteLine();
                Console.WriteLine();
                _output.RenderDrinkDetailKeyOptions();

                var keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.V:
                        await ManageViewImage(drinkDetailResult.Value.ImageUrl);
                        break;
                    case ConsoleKey.F:
                        await ManageAddFavorite(drinkDetailResult.Value);
                        break;
                    case ConsoleKey.D:
                        exitCode = ExitCode.DrinkSelection;
                        break;
                    case ConsoleKey.C:
                        exitCode = ExitCode.CategorySelection;
                        break;
                    case ConsoleKey.M:
                        exitCode = ExitCode.MainMenu;
                        break;
                    default:
                        _output.InputErrorMessage();
                        exitCode = ExitCode.None;
                        break;
                }
            }
            else
            {
                _output.OutputErrorMessage(drinkDetailResult.Errors);
                _input.PressAnyKeyToContinue();
            }

            if (exitCode == ExitCode.DrinkSelection || exitCode == ExitCode.CategorySelection || exitCode == ExitCode.MainMenu)
                returnToDrinkSelection = true;
            else
                returnToDrinkSelection = false;
        }

        return exitCode;
    }

    private async Task ManageViewImage(string url)
    {
        Console.Clear();
        var imageResult = await _getDrinkImageHandler.HandleAsync(url);

        if (imageResult.IsSuccess && imageResult.Value != null)
        {
            Console.Clear();
            _drinkImage.Render(imageResult.Value);
            _input.PressAnyKeyToContinue();
        }
        else
        {
            _output.OutputErrorMessage(imageResult.Errors);
            _input.PressAnyKeyToContinue();
        }
    }

    private async Task ManageAddFavorite(DrinkDetailResponse drinkDetails)
    {
        var addResult = await _addFavoriteHandler.HandleAsync(new(drinkDetails.Id, drinkDetails.Name, drinkDetails.Category));

        if (addResult.IsSuccess)
            _output.PrintSuccessMessage(drinkDetails.Name);
        else
            _output.OutputErrorMessage(addResult.Errors);

        _input.PressAnyKeyToContinue();
    }
}
