using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DrinksInfo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await MSTutorial();
        }


        static async Task MSTutorial()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                var repositories = await ProcessRepositoriesAsync(client);

                PrintRepositories(repositories);
            }
        }

        static async Task<List<Repository>> ProcessRepositoriesAsync(HttpClient client)
        {
            var repositories = await client.GetFromJsonAsync<List<Repository>>("https://api.github.com/orgs/dotnet/repos");

            return repositories ?? new();
        }

        static void PrintRepositories(List<Repository> repositories)
        {
            foreach (var repo in repositories)
            {
                Console.WriteLine($"Name: {repo.Name}");
                Console.WriteLine($"Homepage: {repo.Homepage}");
                Console.WriteLine($"GitHub: {repo.GitHubHomeUrl}");
                Console.WriteLine($"Description: {repo.Description}");
                Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
                Console.WriteLine($"Last push: {repo.LastPush}");
                Console.WriteLine();
            }
        }

    }
}
