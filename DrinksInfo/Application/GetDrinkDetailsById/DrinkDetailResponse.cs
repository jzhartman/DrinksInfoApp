namespace DrinksInfo.Application.GetDrinkDetailsById;

public record DrinkDetailResponse(
    int Id,
    string Name,
    string ImageUrl,
    string Category,
    bool IsAlcoholic,
    string Glass,
    string Insructions,
    List<string> Ingredients,
    List<string> Measurements);