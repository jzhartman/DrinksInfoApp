namespace DrinksInfo.Domain.Entities;

public class DrinkViewCount
{
    public int Id { get; set; }
    public int DrinkId { get; set; }
    public long ViewCount { get; set; }
}
