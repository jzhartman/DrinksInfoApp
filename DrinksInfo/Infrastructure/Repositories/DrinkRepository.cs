using DrinksInfo.Application.GetDrinkImage;
using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;
using DrinksInfo.Infrastructure.Models;
using SixLabors.ImageSharp;
using System.Net.Http.Json;
using System.Text.Json;

namespace DrinksInfo.Infrastructure.Repositories;

public class DrinkRepository : IDrinkRepository
{
    private readonly HttpClient _client;
    public DrinkRepository(HttpClient client)
    {
        _client = client;
    }
    public async Task<Result<List<Category>>> GetCategoryListAsync()
    {
        try
        {
            var response = await _client.GetFromJsonAsync<CategoryListApiResponse>("list.php?c=list");

            if (response?.Drinks is null)
                return Result<List<Category>>.Failure(Errors.EmptyResponse);

            var categories = response.Drinks
                .Select(c => new Category(c.Name))
                .ToList();

            return Result<List<Category>>.Success(categories);
        }
        catch (HttpRequestException)
        {
            return Result<List<Category>>.Failure(Errors.NetworkError);
        }
        catch (TaskCanceledException)
        {
            return Result<List<Category>>.Failure(Errors.Timeout);
        }
        catch (JsonException)
        {
            return Result<List<Category>>.Failure(Errors.InvalidJson);
        }
    }

    public async Task<List<DrinkSummary>> GetDrinkListByCategoryNameAsync(string categoryName)
    {
        var result = await _client.GetFromJsonAsync<DrinkListApiResponse>(
            $"filter.php?c={categoryName.Replace(" ", "_")}");

        return result.Drinks
            .Select(d => new DrinkSummary(d.idDrink, d.strDrink, d.strDrinkThumb, categoryName))
            .ToList();
    }

    public async Task<Drink> GetDrinkDeailsByIdAsync(int id)
    {
        var url = $"lookup.php?i={id}";

        var result = await _client.GetFromJsonAsync<DrinkDetailsApiResponse>(url);
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
            else
            {
                output.Add("<empty>");
            }
        }
        return output;
    }
}
