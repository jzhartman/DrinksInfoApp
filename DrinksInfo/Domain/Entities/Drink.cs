namespace DrinksInfo.Domain.Entities;

internal class Drink
{
    public DrinkSummary Summary { get; set; }
    public List<Ingredient> Ingredients { get; set; }
}
