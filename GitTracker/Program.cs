using System.Text.Json;
using System.Text.Json.Nodes;
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
            ParseArgs(output);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            
        }
    }

    static void ParseArgs(string output)
    {
        var events = JsonSerializer.Deserialize<List<GithubEvent>>(output);
        Console.WriteLine("output:\n");
        
        int pushCounter = 1;
        string currentRepo = "";
        string currentBranch = "";
        
        foreach (var e in events)
        {
            string type = e.Type;
            string repo = e.Repo.Name;
            string time = e.CreatedAt;

            switch (type)
            {
                case "PushEvent":
                    string branch = e.Payload.Ref?.Replace("refs/heads/", "") ?? "";
                    if (repo == currentRepo && branch == currentBranch)
                    {
                        pushCounter++;
                    }
                    else 
                    {
                        if (currentRepo != "")
                        {
                            Console.WriteLine($"    -Made {pushCounter} push(-es) in repo {currentRepo} -> {currentBranch} branch");
                        }
                        currentRepo = repo;
                        currentBranch = branch;
                        pushCounter = 1;
                    }
                    break;
                
                case "WatchEvent":
                    Console.WriteLine($"    -Starred {repo} at {time}");
                    break;
                
                case "ForkEvent":
                    var source = e.Payload.Forkee?.FullName;
                    Console.WriteLine($"    -Forked {source} repo");
                    break;
                
                case "IssueCommentEvent":
                    var issue = e.Payload.Issue?.Title;
                    var comment = e.Payload.Comment?.Body;
                    Console.WriteLine($"    -Commented Issue: {issue} - {comment}");
                    break;
                
                case "IssuesEvent":
                    var issueAction = e.Payload.Action;
                    var title = e.Payload.Issue?.Title;
                    Console.WriteLine($"    -Issue Action: {issueAction} - {title}");
                    break;
                
                case "CreateEvent":
                    var reftype = e.Payload.RefType;
                    var refname = e.Payload.Ref;
                    Console.WriteLine($"    -Created a new {reftype} - '{refname}'");
                    break;
                
                case "PullRequestEvent":
                    var prAction =  e.Payload.Action;
                    var prName = e.Payload.PullRequest?.Title;
                    Console.WriteLine($"    -Pull Request Action: {prAction} - {prName}");
                    break;
                
                case "DeleteEvent":
                    var deleteRefType = e.Payload.RefType;
                    var deleteRefName = e.Payload.Ref;
                    Console.WriteLine($"    -Deleted:  {deleteRefType} - {deleteRefName}");
                    break;
                
                case "CommitCommentEvent":
                    var commitComment = e.Payload.Comment?.Body;
                    var commentAction =  e.Payload.Action;
                    Console.WriteLine($"    -Commented commit : {commitComment} - {commentAction}");
                    break;
            }
        }
        if (currentRepo != "")
        {
            Console.WriteLine($"    -Made {pushCounter} push(-es) in repo {currentRepo} -> {currentBranch} branch");
        } 
    }
   
}