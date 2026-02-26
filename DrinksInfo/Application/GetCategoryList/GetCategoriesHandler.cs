using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;

namespace DrinksInfo.Application.GetCategories;

internal class GetCategoriesHandler
{
    private readonly IDrinkRepository _drinkRepo;

    public GetCategoriesHandler(IDrinkRepository drinkRepo)
    {
        _drinkRepo = drinkRepo;
    }

    public async Task<List<CategoryListResponse>> HandleAsync()
    {
        var result = await _drinkRepo.GetCategoryListAsync();

        return await MapToResponseAsync(result);
    }

    private async Task<List<CategoryListResponse>> MapToResponseAsync(List<Category> categories)
    {
        var output = new List<CategoryListResponse>();

        foreach (var category in categories)
        {
            output.Add(new CategoryListResponse(category.Name));
        }
        return output;
    }
}
