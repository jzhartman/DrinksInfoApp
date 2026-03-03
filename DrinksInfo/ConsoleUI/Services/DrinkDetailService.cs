using DrinksInfo.Application.GetDrinkDetailsById;
using DrinksInfo.Application.GetDrinkImage;
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

    public async Task<bool> ManageDrinkDetails(int drinkSelection)
    {
        bool returnToCategorySelection = false;
        bool returnToDrinkSelection = false;

        while (returnToDrinkSelection == false)
        {
            Console.Clear();

            var drink = await _getDrinkDetailsHandler.HandleAsync(drinkSelection);
            _drinkDetails.Render(drink);

            Console.WriteLine();
            Console.WriteLine();
            _output.RenderDrinkDetailKeyOptions();

            var keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.V:
                    Console.Clear();
                    var imageData = await _getDrinkImageHandler.Handle(drink.ImageUrl);
                    Console.Clear();
                    _drinkImage.Render(imageData);
                    _input.PressAnyKeyToContinue();
                    break;
                case ConsoleKey.D:
                    returnToCategorySelection = false;
                    returnToDrinkSelection = true;
                    break;
                case ConsoleKey.C:
                    returnToCategorySelection = true;
                    returnToDrinkSelection = true;
                    return true;
                default:
                    _output.InputErrorMessage();
                    returnToCategorySelection = false;
                    returnToDrinkSelection = false;
                    break;
            }
        }

        return returnToCategorySelection;
    }
}
