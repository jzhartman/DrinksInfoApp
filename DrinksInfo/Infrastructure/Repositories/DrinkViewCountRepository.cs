using Dapper;
using DrinksInfo.Domain.Entities;
using DrinksInfo.Domain.Validation;
using DrinksInfo.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace DrinksInfo.Infrastructure.Repositories;

public class DrinkViewCountRepository : IDrinkViewCountRepository
{
    private readonly ISqliteConnectionFactory _connection;

    public DrinkViewCountRepository(ISqliteConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<Result<DrinkViewCount>> GetCountByIdAsync(int id)
    {
        string sql = $"Select * from DrinkViewCount where DrinkId = @Id";

        try
        {
            using var connection = _connection.CreateConnection();

            var response = await connection.ExecuteScalarAsync<DrinkViewCount>(sql, new { Id = id });

            if (response == null)
                return Result<DrinkViewCount>.Failure(Errors.GenericNull);

            return Result<DrinkViewCount>.Success(response);

        }
        catch (SqliteException ex)
        {
            return Result<DrinkViewCount>.Failure(new Error(ex.ErrorCode.ToString(), ex.Message));
        }
    }

    public async Task<Result> UpdateCountByIdAsync(int id)
    {
        string sql = @$"update DrinkViewCount
                        set ViewCount = ViewCount + 1
                        where DrinkId = @Id";

        try
        {
            using var connection = _connection.CreateConnection();

            var response = await connection.ExecuteScalarAsync(sql, new { Id = id });

            if (response == null)
                return Result.Failure(Errors.GenericNull);

            return Result.Success();

        }
        catch (SqliteException ex)
        {
            return Result.Failure(new Error(ex.ErrorCode.ToString(), ex.Message));
        }
    }

    public async Task<Result> ExistsByIdAsync(int id)
    {
        string sql = $"Select count(1) from DrinkViewCount where DrinkId = @Id";

        try
        {
            using var connection = _connection.CreateConnection();

            var response = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

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

    public async Task<Result> AddByDrinkId(DrinkViewCount viewCount)
    {
        string sql = "insert into DrinkViewCount (DrinkId, ViewCount) values (@DrinkId, 0)";

        try
        {
            using var connection = _connection.CreateConnection();
            connection.Execute(sql, viewCount);

            var result = await ExistsByIdAsync((int)viewCount.DrinkId);

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

}
