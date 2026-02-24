namespace DrinksInfo.Domain.Entities;

public class Ingredient
{
    string Name { get; set; }
    string Measurement { get; set; }
    public Ingredient(string name, string measurement)
    {
        Name = name;
        Measurement = measurement;
    }
}
