using System.Text.Json.Serialization;

namespace GitTracker;

public class Payload
{
    [JsonPropertyName("ref")]
    public string? Ref { get; set; }
    
    [JsonPropertyName("ref_type")]
    public string? RefType { get; set; }

    [JsonPropertyName("forkee")]
    public Forkee? Forkee { get; set; }
    
    [JsonPropertyName("issue")]
    public Issue? Issue { get; set; }
    
    [JsonPropertyName("comment")]
    public Comment? Comment { get; set; }
    
   
    [JsonPropertyName("action")]
    public string? Action { get; set; }
    
    [JsonPropertyName("pull_request")]
    public PullRequest? PullRequest { get; set; }
}