using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Initializer : Node
    {
        [JsonPropertyName("value")] public string Value { get; set; }
         [JsonProperty("!")]
        public string  { get; set; }

        [JsonProperty("range")]
        public Range { get; set; }

        [JsonProperty("tokenRange")]
        public TokenRange TokenRange { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("expression")]
        public Expression Expression { get; set; }

        [JsonProperty("arguments")]
        public List<Argument> Arguments { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("scope")]
        public Scope Scope { get; set; }



    }
}