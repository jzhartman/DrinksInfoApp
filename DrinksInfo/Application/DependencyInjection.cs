using DrinksInfo.Application.GetCategories;
using DrinksInfo.Application.GetDrinkDetailsById;
using DrinksInfo.Application.GetDrinkImage;
using DrinksInfo.Application.GetDrinksFromCategory;
using DrinksInfo.Application.Interfaces;
using DrinksInfo.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace DrinksInfo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHttpClient<IDrinkRepository, DrinkRepository>(client =>
        {
            client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddTransient<GetCategoryListHandler>();
        services.AddTransient<GetDrinksSummaryByCategoryNameHandler>();
        services.AddTransient<GetDrinkDetailsByIdHandler>();
        services.AddTransient<GetDrinkImageHandler>();

        return services;
    }
}
