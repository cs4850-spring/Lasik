using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Modifier : Node
    {
        [JsonPropertyName("keyword")] public string Keyword { get; set; }
    }
}