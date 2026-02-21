using DrinksInfo.Application.Interfaces;
using DrinksInfo.ConsoleUI.Models;
using DrinksInfo.Domain.Entities;

namespace DrinksInfo.Application.GetCategories;

internal class GetCategoriesHandler
{
    private readonly IRepository _repo;

    public GetCategoriesHandler(IRepository repo)
    {
        _repo = repo;
    }

    internal async Task<List<CategoryViewModel>> Handle()
    {
        var categories = _repo.GetCategoriesAsync();

        return MapResponse(await categories);
    }

    private List<CategoryViewModel> MapResponse(List<Category> categories)
    {
        var response = new List<CategoryViewModel>();
        foreach (var category in categories)
        {
            response.Add(new(category.strCategory));
        }
        return response;
    }
}
