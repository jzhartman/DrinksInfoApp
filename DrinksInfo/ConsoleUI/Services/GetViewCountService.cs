using DrinksInfo.Application.ViewCount.AddByDrinkId;
using DrinksInfo.Application.ViewCount.ExistsById;
using DrinksInfo.Application.ViewCount.GetById;
using DrinksInfo.Application.ViewCount.UpdateById;

namespace DrinksInfo.ConsoleUI.Services;

public class GetViewCountService
{
    private readonly ViewCountExistsByIdHandler _viewCountExists;
    private readonly UpdateViewCountByIdHandler _updateViewCountHandler;
    private readonly GetViewCountByIdHandler _getViewCountHandler;
    private readonly AddViewCountByDrinkIdHandler _addViewCountHandler;
    public GetViewCountService(ViewCountExistsByIdHandler viewCountExists, UpdateViewCountByIdHandler updateViewCountHandler,
                                GetViewCountByIdHandler getViewCountHandler, AddViewCountByDrinkIdHandler addViewCountHandler)
    {
        _viewCountExists = viewCountExists;
        _updateViewCountHandler = updateViewCountHandler;
        _getViewCountHandler = getViewCountHandler;
        _addViewCountHandler = addViewCountHandler;
    }
    public async Task<int> RunAsync(int drinkId)
    {
        bool viewCountExists = (await _viewCountExists.HandleAsync(drinkId)).IsSuccess;

        if (viewCountExists)
        {
            var updateCountResult = await _updateViewCountHandler.HandleAsync(drinkId);

            if (updateCountResult.IsSuccess)
            {
                var countResult = await _getViewCountHandler.HandleAsync(drinkId);

                if (countResult.IsSuccess)
                    return countResult.Value;
                else
                    return -1;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            await _addViewCountHandler.HandleAsync(drinkId);
            return 1;
        }
    }
}
