using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;

namespace DrinksInfo.Application.GetDrinksFromCategory;

internal class GetDrinksSummaryByCategoryNameHandler
{
    private readonly IDrinkRepository _drinkRepo;
    public GetDrinksSummaryByCategoryNameHandler(IDrinkRepository drinkRepo)
    {
        _drinkRepo = drinkRepo;
    }

    public async Task<List<DrinkSummaryResponse>> Handle(string categoryName)
    {
        var result = await _drinkRepo.GetDrinkListByCategoryName(categoryName);

        return await MapToResponse(result);
    }

    private async Task<List<DrinkSummaryResponse>> MapToResponse(List<DrinkSummary> drinks)
    {
        var output = new List<DrinkSummaryResponse>();

        foreach (var drink in drinks)
        {
            output.Add(new DrinkSummaryResponse(drink.Id, drink.Name, drink.ImageUrl, drink.Category));
        }
        return output;
    }
}