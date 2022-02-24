using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Root : Node
    {
       [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("tokenRange")]
        public TokenRange TokenRange { get; set; }

        [JsonProperty("imports")]
        public List<object> Imports { get; set; }

        [JsonProperty("types")]
        public List<Type> Types { get; set; }
    }
}