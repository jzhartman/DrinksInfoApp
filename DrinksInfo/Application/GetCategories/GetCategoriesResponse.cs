using System.Text.Json.Serialization;

namespace DrinksInfo.Application.GetCategories;

internal record GetCategoriesResponse([property: JsonPropertyName("strCategory")] string Name);
