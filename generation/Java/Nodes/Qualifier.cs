using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Qualifier : Node
    {
        [JsonPropertyName("identifier")] public string Identifier { get; set; }
    }
}