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
                string? type = element.GetProperty("type").GetString();
                string? repo = element.GetProperty("repo").GetProperty("name").GetString();
                string? time = element.GetProperty("created_at").GetString();
                var payload = element.GetProperty("payload");

                
                
                switch (type)
                {
                    case "PushEvent":
                        string branch = payload.GetProperty("ref").GetString()!.Replace("refs/heads/", "");
                        
                        if (repo == currentRepo && branch == currentBranch)
                        {
                            pushCounter++;
                        }
                        else
                        {
                            if (currentRepo != "")
                            {
                                Console.WriteLine($"    -Made {pushCounter} push(-es) in repo {repo} -> {branch} branch at {time}");
                            }
                            currentRepo = repo;
                            currentBranch = branch;
                            pushCounter = 1;
                        }
                        break;
                    
                    case "WatchEvent":
                        Console.WriteLine($"    -Starred the {repo} at {time}");
                        break;
                    
                    case "ForkEvent":
                        var source = payload.GetProperty("full_name").GetString();
                        Console.WriteLine($"    -Forked the {source}");
                        break;
                    case "IssueCommentEvent":
                        var issue = payload.GetProperty("issue").GetString();
                        var comment = payload.GetProperty("comment").GetString();
                        Console.WriteLine($"    -Issued {repo}: {issue} -> {comment}");
                        break;
                }
                if (currentRepo != "")
                {
                    Console.WriteLine($"    -Made {pushCounter} push(-es) in repo {repo} -> {branch} branch at {time}");
                }
                

                
            }
        }
    }
}