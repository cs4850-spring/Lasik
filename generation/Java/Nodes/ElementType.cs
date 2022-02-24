using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class ElementType : Node
    {
        [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("tokenRange")]
        public TokenRange TokenRange { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("annotations")]
        public List<object> Annotations { get; set; }
    }
}