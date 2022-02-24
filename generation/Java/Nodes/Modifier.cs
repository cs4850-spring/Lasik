using System.Text.Json.Serialization;

namespace Generation.Java.Nodes
{
    public class Modifier : Node
    {
        [JsonPropertyName("keyword")] public string Keyword { get; set; }
    }
}