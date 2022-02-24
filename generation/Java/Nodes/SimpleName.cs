using System.Text.Json.Serialization;

namespace Generation.Java.Nodes
{
    public class SimpleName : Node
    {
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
        
        [JsonPropertyName("qualifier")]
        public Qualifier? Qualifier { get; set; }
    }
}