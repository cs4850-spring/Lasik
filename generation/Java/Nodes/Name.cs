using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Name : Node
    {
        [JsonPropertyName("identifier")] public string Identifier { get; set; }

        [JsonPropertyName("qualifier")] public Qualifier Qualifier { get; set; }
    }
}