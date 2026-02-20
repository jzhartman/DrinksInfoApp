using DrinksInfo.Domain.Entities;

namespace DrinksInfo.Application.Interfaces;

internal interface IRepository
{
    Task<List<Category>> GetCategoriesAsync();
}

