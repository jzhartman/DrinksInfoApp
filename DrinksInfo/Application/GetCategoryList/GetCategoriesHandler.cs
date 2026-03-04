using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.GetCategories;

internal class GetCategoriesHandler
{
    private readonly IDrinkRepository _drinkRepo;

    public GetCategoriesHandler(IDrinkRepository drinkRepo)
    {
        _drinkRepo = drinkRepo;
    }

    public async Task<Result<List<CategoryListResponse>>> HandleAsync()
    {
        var result = await _drinkRepo.GetCategoryListAsync();

        if (result.IsFailure)
            return Result<List<CategoryListResponse>>.Failure(result.Errors.ToArray());
        if (result?.Value == null)
            return Result<List<CategoryListResponse>>.Failure(Errors.GenericNull);
        else
            return Result<List<CategoryListResponse>>.Success(await MapToResponseAsync(result.Value));
    }

    private static async Task<List<CategoryListResponse>> MapToResponseAsync(List<Category> categories)
    {
        var output = new List<CategoryListResponse>();

        foreach (var category in categories)
        {
            output.Add(new CategoryListResponse(category.Name));
        }
        return output;
    }
}
