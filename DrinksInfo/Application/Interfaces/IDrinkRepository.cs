using DrinksInfo.Application.GetDrinkImage;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;

namespace DrinksInfo.Application.Interfaces;

public interface IDrinkRepository
{
    Task<Result<List<Category>>> GetCategoryListAsync();
    Task<Drink> GetDrinkDeailsByIdAsync(int id);
    Task<DrinkImageResponse> GetDrinkImageAsync(string url);
    Task<List<DrinkSummary>> GetDrinkListByCategoryNameAsync(string categoryName);
}