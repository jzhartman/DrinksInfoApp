using DrinksInfo.Application.Interfaces;

namespace DrinksInfo.ConsoleUI.Services;

public class FavoriteDrinkServices
{
    private readonly IFavoriteDrinkRepository _favoriteDrinkRepo;
    private readonly IDrinkRepository _drinkRepo;

    public FavoriteDrinkServices(IFavoriteDrinkRepository favoriteDrinkRepo, IDrinkRepository drinkRepo)
    {
        _favoriteDrinkRepo = favoriteDrinkRepo;
        _drinkRepo = drinkRepo;
    }

    public async Task RunAsync()
    {

    }
}
