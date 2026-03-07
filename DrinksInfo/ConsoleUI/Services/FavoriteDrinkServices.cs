using DrinksInfo.Application.Favorites.GetAllFavoriteDrinks;
using DrinksInfo.Application.Interfaces;
using DrinksInfo.ConsoleUI.Helpers;

namespace DrinksInfo.ConsoleUI.Services;

public class FavoriteDrinkServices
{
    private readonly IFavoriteDrinkRepository _favoriteDrinkRepo;
    private readonly IDrinkRepository _drinkRepo;

    private readonly GetAllFavoriteDrinksHandler _getAllFavoriteDrinksHandler;

    public FavoriteDrinkServices(IFavoriteDrinkRepository favoriteDrinkRepo, IDrinkRepository drinkRepo,
                                GetAllFavoriteDrinksHandler getAllFavoriteDrinksHandler)
    {
        _favoriteDrinkRepo = favoriteDrinkRepo;
        _drinkRepo = drinkRepo;
        _getAllFavoriteDrinksHandler = getAllFavoriteDrinksHandler;
    }

    public async Task RunAsync()
    {
        Console.Clear();
        var favoriteListResult = await ConsoleStatusHelper.ShowStatusAsync("Fetching favorite drink list...", () =>
                                                _getAllFavoriteDrinksHandler.HandleAsync());

        if (favoriteListResult.IsSuccess && favoriteListResult.Value != null)
        {
            // Print Favorite List as Selection Prompt
            // Get user selection
            // 
        }
        else
        {
            //Print Errors
        }
    }
}
