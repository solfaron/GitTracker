using System.Text.Json.Serialization;

namespace GitTracker;

public class GithubEvent
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("repo")]
    public Repo Repo { get; set; }
    
    [JsonPropertyName("time")]
    public string CreatedAt { get; set; }
    
    [JsonPropertyName("payload")]
    public Payload Payload { get; set; }
}