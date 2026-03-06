using Dapper;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Infrastructure.Interfaces;

namespace DrinksInfo.Infrastructure.Repositories;

public class FavoriteDrinkRepository
{
    private readonly ISqliteConnectionFactory _connectionFactory;
    public FavoriteDrinkRepository(ISqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<DrinkSummary>> GetAll()
    {
        string sql = "Select * from FavoriteDrink";

        using var connection = _connectionFactory.CreateConnection();
        List<DrinkSummary> drinks = connection.Query<DrinkSummary>(sql).ToList();
        return drinks;
    }

    public async Task Add()
    {

    }
}
