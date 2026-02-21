using DrinksInfo.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace DrinksInfo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IRepository, Repository>();
        services.AddHttpClient<IRepository, Repository>(client =>
        {
            client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/json/v1/1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        });


        return services;
    }
}
