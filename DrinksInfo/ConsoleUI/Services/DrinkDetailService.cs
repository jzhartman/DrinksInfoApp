using DrinksInfo.Application.GetDrinkDetailsById;
using DrinksInfo.Application.GetDrinkImage;
using DrinksInfo.ConsoleUI.Enums;
using DrinksInfo.ConsoleUI.Input;
using DrinksInfo.ConsoleUI.Output;
using DrinksInfo.ConsoleUI.Views;

namespace DrinksInfo.ConsoleUI.Services;

public class DrinkDetailService
{
    private readonly GetDrinkDetailsByIdHandler _getDrinkDetailsHandler;
    private readonly GetDrinkImageHandler _getDrinkImageHandler;
    private readonly DrinkDetailsView _drinkDetails;
    private readonly DrinkImageView _drinkImage;
    private readonly ConsoleOutput _output;
    private readonly UserInput _input;


    public DrinkDetailService(GetDrinkDetailsByIdHandler getDrinkDetailsHandler, GetDrinkImageHandler getDrinkImageHandler,
                                DrinkDetailsView drinkDetails, DrinkImageView drinkImage, ConsoleOutput output, UserInput input)
    {
        _getDrinkDetailsHandler = getDrinkDetailsHandler;
        _getDrinkImageHandler = getDrinkImageHandler;
        _drinkDetails = drinkDetails;
        _drinkImage = drinkImage;
        _output = output;
        _input = input;
    }

    public async Task<ExitCode> ManageDrinkDetailsAsync(int drinkSelection)
    {
        var exitCode = ExitCode.None;
        bool returnToDrinkSelection = false;

        while (returnToDrinkSelection == false)
        {
            Console.Clear();

            var drinkDetailResult = await _getDrinkDetailsHandler.HandleAsync(drinkSelection);

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
                        Console.WriteLine("Someday this will add it to your favorites... Until then, try to remember it!");
                        _input.PressAnyKeyToContinue();
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
}
