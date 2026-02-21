using DrinksInfo.Application;
using DrinksInfo.ConsoleUI.Services;
using DrinksInfo.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DrinksInfo.ConsoleUI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddApplication();
            services.AddInfrastructure();
            services.AddConsoleUI();
            var provider = services.BuildServiceProvider();

            var mainMenu = provider.GetRequiredService<MainMenuService>();
            mainMenu.Run();

        }
    }
}




//    static async Task MSTutorial()
//    {
//        using (var client = new HttpClient())
//        {
//            client.DefaultRequestHeaders.Accept.Clear();
//            client.DefaultRequestHeaders.Accept.Add(
//                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
//            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

//            var repositories = await ProcessRepositoriesAsync(client);

//            PrintRepositories(repositories);
//        }
//    }
//    static async Task<List<Repository>> ProcessRepositoriesAsync(HttpClient client)
//    {
//        var repositories = await client.GetFromJsonAsync<List<Repository>>("https://api.github.com/orgs/dotnet/repos");

//        return repositories ?? new();
//    }

//    static void PrintRepositories(List<Repository> repositories)
//    {
//        foreach (var repo in repositories)
//        {
//            Console.WriteLine($"Name: {repo.Name}");
//            Console.WriteLine($"Homepage: {repo.Homepage}");
//            Console.WriteLine($"GitHub: {repo.GitHubHomeUrl}");
//            Console.WriteLine($"Description: {repo.Description}");
//            Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
//            Console.WriteLine($"Last push: {repo.LastPush}");
//            Console.WriteLine();
//        }
//    }
