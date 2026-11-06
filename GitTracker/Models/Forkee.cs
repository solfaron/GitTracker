using System.Text.Json.Serialization;

namespace GitTracker;

public class Forkee
{
    [JsonPropertyName("full_name")]
    public string FullName { get; set; }
}