using System.Text.Json;
using System.Text.Json.Nodes;

class Program
{
    private static readonly HttpClient Client = new ();
    static async Task Main(string[] args)
    {
        string nickname = Console.ReadLine();
        
        
        Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        try
        {
            var response = await Client.GetAsync($"https://api.github.com/users/{nickname}/events"); 
            response.EnsureSuccessStatusCode();
            var output = await response.Content.ReadAsStringAsync();
            ParseArgs(output);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            
        }
    }

    static void ParseArgs(string output)
    {
        using (JsonDocument json = JsonDocument.Parse(output))
        {
            var elements = json.RootElement.EnumerateArray();
            foreach (var element in elements)
            {
                Console.WriteLine(element.GetProperty("type").GetString());
                Console.WriteLine(element.GetProperty("repo").GetProperty("name").GetString());
            }
        }
    }
}