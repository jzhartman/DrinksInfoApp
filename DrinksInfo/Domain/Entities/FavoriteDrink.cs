namespace DrinksInfo.Domain.Entities;

public class FavoriteDrink
{
    public int Id { get; set; }
    public int DrinkId { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }

    public FavoriteDrink(int id, int drinkId, string name, string category)
    {
        Id = id;
        DrinkId = drinkId;
        Name = name;
        Category = category;
    }
}
