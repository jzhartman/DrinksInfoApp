using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;
using System.Net.Http.Json;

namespace DrinksInfo.Infrastructure;

internal class Repository : IRepository
{
    private readonly HttpClient _httpClient;

    public Repository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        var url = "https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list";

        var response = await _httpClient.GetFromJsonAsync<GetCategoriesResponse>(url);

        return response?.Drinks ?? new List<Category>();
    }
}

internal record GetCategoriesResponse(List<Category> Drinks);
