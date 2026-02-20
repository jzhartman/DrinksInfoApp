using DrinksInfo.Application.Interfaces;

namespace DrinksInfo.Application.GetCategories;

internal class GetCategoriesHandler
{
    private readonly IRepository _repo;

    public GetCategoriesHandler(IRepository repo)
    {
        _repo = repo;
    }

    internal async Task<List<GetCategoriesResponse> Handle()
    {
        var categories = _repo.GetCategoriesAsync();
    }
}
