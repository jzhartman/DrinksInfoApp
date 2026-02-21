using DrinksInfo.Domain.Entities;

namespace DrinksInfo.Application.GetCategories;

internal record GetCategoriesResponse(List<Category> Drinks);