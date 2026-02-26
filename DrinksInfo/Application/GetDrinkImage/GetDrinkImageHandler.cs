using DrinksInfo.Application.Interfaces;

namespace DrinksInfo.Application.GetDrinkImage;

public class GetDrinkImageHandler
{
    private readonly IDrinkRepository _drinkRepo;

    public GetDrinkImageHandler(IDrinkRepository drinkRepo)
    {
        _drinkRepo = drinkRepo;
    }

    public Task<DrinkImageResponse> Handle(string url)
    {
        return _drinkRepo.GetDrinkImageAsync(url);
    }
}
