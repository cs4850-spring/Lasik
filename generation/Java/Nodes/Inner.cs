sing System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Inner : Node
    {
        [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("tokenRange")]
        public TokenRange TokenRange { get; set; }

        [JsonProperty("left")]
        public Left Left { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("right")]
        public Right Right { get; set; }
    }
}