using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DrinksInfo.Application.GetCategories;

internal class GetCategoriesHandler
{
    public async Task<List<GetCategoriesResponse>> Handle()
    {
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var result = await client.GetFromJsonAsync<GetCategoriesApiResponse>("https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");

        return await MapToResponse(result.Drinks);

    }

    private async Task<List<GetCategoriesResponse>> MapToResponse(List<CategoryApi> categories)
    {
        var output = new List<GetCategoriesResponse>();

        foreach (var category in categories)
        {
            output.Add(new GetCategoriesResponse(category.Name));
        }
        return output;
    }
}
