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
            Console.WriteLine("output:\n");
            
            int pushCounter = 1;
            string currentRepo = "";
            string currentBranch = "";

            foreach (var element in elements)
            {
                string type = element.GetProperty("type").GetString();
                string repo = element.GetProperty("repo").GetProperty("name").GetString();
                string time = element.GetProperty("created_at").GetString();
                var payload = element.GetProperty("payload");
                string branch = payload.GetProperty("ref").GetString().Replace("refs/heads/", "");

                
                
                switch (type)
                {
                    case "PushEvent":
                        if (repo == currentRepo && branch == currentBranch)
                        {
                            pushCounter++;
                        }
                        
                        else
                        {
                            Console.WriteLine($"  -Made {pushCounter} push(-es) in repo {repo} -> {branch} branch");
                            currentRepo = repo;
                            currentBranch = branch;
                            pushCounter = 1;
                        }
                        break;
                    
                    case "WatchEvent":
                        break;
                        
                }

                
            }
        }
    }
}