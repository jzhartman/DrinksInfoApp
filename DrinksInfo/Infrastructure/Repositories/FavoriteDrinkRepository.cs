using Dapper;
using DrinksInfo.Application.Interfaces;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;
using DrinksInfo.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace DrinksInfo.Infrastructure.Repositories;

public class FavoriteDrinkRepository : IFavoriteDrinkRepository
{
    private readonly ISqliteConnectionFactory _connectionFactory;
    public FavoriteDrinkRepository(ISqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<Result> ExistsByIdAsync(int id)
    {
        string sql = "Select count(1) from FavoriteDrink where DrinkId = @Id";

        try
        {
            using var connection = _connectionFactory.CreateConnection();

            var response = await connection.ExecuteScalarAsync<int>(sql);

            if (response > 0)
                return Result.Success();
            else
                return Result.Failure(Errors.NoRecordById);
        }
        catch (SqliteException ex)
        {
            return Result.Failure(new Error(ex.ErrorCode.ToString(), ex.Message));
        }
    }
    public async Task<Result<List<FavoriteDrink>>> GetAllAsync()
    {
        string sql = "Select * from FavoriteDrink";

        try
        {
            using var connection = _connectionFactory.CreateConnection();

            var response = await connection.QueryAsync<FavoriteDrink>(sql);

            if (response.Count() == 0)
                return Result<List<FavoriteDrink>>.Failure(Errors.EmptyDbResponse);

            return Result<List<FavoriteDrink>>.Success(response.ToList());
        }
        catch (SqliteException ex)
        {
            return Result<List<FavoriteDrink>>.Failure(new Error(ex.ErrorCode.ToString(), ex.Message));
        }
    }

    public async Task<Result> AddAsync(FavoriteDrink drink)
    {
        string sql = "insert into FavoriteDrink (DrinkId, Name, Category) values (@DrinkId, @Name, @Category)";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute(sql, drink);

            var result = await ExistsByIdAsync(drink.DrinkId);

            if (result.IsSuccess)
                return Result.Success();
            else
                return Result.Failure(Errors.AddFailed);
        }
        catch (SqliteException ex)
        {
            return Result.Failure(new Error(ex.ErrorCode.ToString(), ex.Message));
        }
    }

    public async Task<Result> DeleteByIdAsync(int id)
    {
        string sql = $"delete from FavoriteDrink where Id = {id}";

        try
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Execute(sql);

            var result = await ExistsByIdAsync(id);

            if (result.IsFailure)
                return Result.Success();
            else
                return Result.Failure(Errors.AddFailed);
        }
        catch (SqliteException ex)
        {
            return Result.Failure(new Error(ex.ErrorCode.ToString(), ex.Message));
        }
    }
}
