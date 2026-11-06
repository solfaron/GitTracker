using System.Text.Json.Serialization;

namespace GitTracker;

public class Comment
{
    [JsonPropertyName("body")]
    public string Body { get; set; }
    
}