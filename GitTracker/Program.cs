using GitTracker;

class Program
{
    private static readonly HttpClient Client = new ();
    static async Task Main()
    {
        string nickname = Console.ReadLine();
        
        
        Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        try
        {
            var response = await Client.GetAsync($"https://api.github.com/users/{nickname}/events"); 
            response.EnsureSuccessStatusCode();
            var output = await response.Content.ReadAsStringAsync();
            var processor = new GithubEventProcessor();
            processor.ProcessAndDisplay(output);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            
        }
    }
}