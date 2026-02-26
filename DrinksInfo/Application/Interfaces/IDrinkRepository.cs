using DrinksInfo.Application.GetDrinkImage;
using DrinksInfo.Domain.Entities;

namespace DrinksInfo.Application.Interfaces;

public interface IDrinkRepository
{
    Task<List<Category>> GetCategoryListAsync();
    Task<Drink> GetDrinkDeailsByIdAsync(int id);
    Task<DrinkImageResponse> GetDrinkImageAsync(string url);
    Task<List<DrinkSummary>> GetDrinkListByCategoryNameAsync(string categoryName);
}