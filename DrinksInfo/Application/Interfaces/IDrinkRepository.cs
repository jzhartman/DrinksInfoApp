using DrinksInfo.Domain.Entities;

namespace DrinksInfo.Application.Interfaces;

public interface IDrinkRepository
{
    Task<List<Category>> GetCategoryList();
    Task<List<DrinkSummary>> GetDrinkListByCategoryName(string categoryName);
}