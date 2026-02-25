using DrinksInfo.Domain.Entities;

namespace DrinksInfo.Application.Interfaces;

public interface IDrinkRepository
{
    Task<List<Category>> GetCategoryList();
    Task<Drink> GetDrinkDeailsById(int id);
    Task<List<DrinkSummary>> GetDrinkListByCategoryName(string categoryName);
}