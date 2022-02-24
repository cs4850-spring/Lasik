using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Dimension : Node
    {
        
     [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("tokenRange")]
        public TokenRange TokenRange { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}