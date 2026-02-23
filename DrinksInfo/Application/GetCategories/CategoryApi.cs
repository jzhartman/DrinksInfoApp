using System.Text.Json.Serialization;

namespace DrinksInfo.Application.GetCategories;

public record CategoryApi([property: JsonPropertyName("strCategory")] string Name);


