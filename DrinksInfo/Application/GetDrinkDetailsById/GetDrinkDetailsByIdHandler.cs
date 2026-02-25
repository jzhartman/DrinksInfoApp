using DrinksInfo.Application.Interfaces;

namespace DrinksInfo.Application.GetDrinkDetailsById;

public class GetDrinkDetailsByIdHandler
{
    private readonly IDrinkRepository _drinkRepo;
    public GetDrinkDetailsByIdHandler(IDrinkRepository drinkRepo)
    {
        _drinkRepo = drinkRepo;
    }

    public async Task<DrinkDetailResponse> Handle(int id)
    {
        var result = await _drinkRepo.GetDrinkDeailsById(id);

        return new DrinkDetailResponse(
            result.Summary.Id,
            result.Summary.Name,
            result.Summary.ImageUrl,
            result.Summary.Category,
            result.IsAlcoholic,
            result.Glass,
            result.Instructions,
            result.Ingredients,
            result.Measurements);
    }

}
