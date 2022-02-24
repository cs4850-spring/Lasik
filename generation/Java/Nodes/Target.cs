using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Target : Node
    {
        [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("tokenRange")]
        public TokenRange TokenRange { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }
    }
}