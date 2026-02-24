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

    public async Task<List<CategoryListResponse>> Handle()
    {
        var result = await _drinkRepo.GetCategoryList();

        return await MapToResponse(result);
    }

    private async Task<List<CategoryListResponse>> MapToResponse(List<Category> categories)
    {
        var output = new List<CategoryListResponse>();

        foreach (var category in categories)
        {
            output.Add(new CategoryListResponse(category.Name));
        }
        return output;
    }
}
