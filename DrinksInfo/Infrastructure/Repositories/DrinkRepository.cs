using DrinksInfo.Application.GetDrinkImage;
using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Infrastructure.Models;
using SixLabors.ImageSharp;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DrinksInfo.Infrastructure.Repositories;

public class DrinkRepository : IDrinkRepository
{
    public async Task<List<Category>> GetCategoryListAsync()
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var result = await client.GetFromJsonAsync<CategoryListApiResponse>(
            "https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");

        return result.Drinks
            .Select(c => new Category(c.Name))
            .ToList();
    }

    public async Task<List<DrinkSummary>> GetDrinkListByCategoryNameAsync(string categoryName)
    {
        categoryName.Replace(" ", "_");

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var result = await client.GetFromJsonAsync<DrinkListApiResponse>(
            $"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={categoryName}");

        return result.Drinks
            .Select(d => new DrinkSummary(d.idDrink, d.strDrink, d.strDrinkThumb, categoryName))
            .ToList();
    }

    public async Task<Drink> GetDrinkDeailsByIdAsync(int id)
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var url = $"https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i={id}";

        var result = await client.GetFromJsonAsync<DrinkDetailsApiResponse>(url);
        var drink = result.Drinks[0];

        var ingredients = ExtractList(drink, "strIngredient", 15);
        var measurements = ExtractList(drink, "strMeasure", 15);
        var isAlcoholic = (drink.strAlcoholic.ToUpper() == "ALCOHOLIC") ? true : false;

        return new Drink(
                new DrinkSummary(drink.idDrink, drink.strDrink, drink.strDrinkThumb, drink.strCategory),
                isAlcoholic,
                drink.strGlass,
                drink.strInstructions,
                ingredients,
                measurements);
    }

    public async Task<DrinkImageResponse> GetDrinkImageAsync(string url)
    {
        using var client = new HttpClient();
        byte[] bytes = await client.GetByteArrayAsync($"{url}/small");

        return new DrinkImageResponse(bytes);
    }

    private List<string> ExtractList(object apiResponse, string prefix, int max)
    {
        var output = new List<string>();

        for (int i = 1; i <= max; i++)
        {
            var property = apiResponse.GetType().GetProperty($"{prefix}{i}");
            var value = property?.GetValue(apiResponse) as string;

            if (!string.IsNullOrWhiteSpace(value))
                output.Add(value);
        }
        return output;
    }
}
