using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.Favorites.ExistsById;

public class FavoriteExistsByIdHandler
{
    private readonly IFavoriteDrinkRepository _favoriteRepository;

    public FavoriteExistsByIdHandler(IFavoriteDrinkRepository favoriteDrinkRepository)
    {
        _favoriteRepository = favoriteDrinkRepository;
    }

    public async Task<Result> HandleAsync(int id)
    {
        return await _favoriteRepository.ExistsByIdAsync(id);
    }
}
