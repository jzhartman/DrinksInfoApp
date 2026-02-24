using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Infrastructure.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DrinksInfo.Infrastructure.Repositories;

public class DrinkRepository : IDrinkRepository
{
    public async Task<List<Category>> GetCategoryList()
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var result = await client.GetFromJsonAsync<CategoryListApiResponse>("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");

        return result.Drinks
            .Select(c => new Category(c.Name))
            .ToList();
    }

    public async Task<List<DrinkSummary>> GetDrinkListByCategoryName(string categoryName)
    {
        categoryName.Replace(" ", "_");

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var result = await client.GetFromJsonAsync<DrinkListApiResponse>($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={categoryName}");

        return result.Drinks
            .Select(d => new DrinkSummary(d.idDrink, d.strDrink, d.strDrinkThumb, categoryName))
            .ToList();
    }
}
