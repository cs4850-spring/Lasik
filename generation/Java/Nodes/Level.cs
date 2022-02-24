using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Level : Node
    {
        [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("tokenRange")]
        public TokenRange TokenRange { get; set; }

        [JsonProperty("annotations")]
        public List<object> Annotations { get; set; }

        [JsonProperty("dimension")]
        public Dimension Dimension { get; set; }get; set; }
    }
}