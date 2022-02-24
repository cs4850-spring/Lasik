using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Update : Node
    {
        
         [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("tokenRange")]
        public TokenRange TokenRange { get; set; }

        [JsonProperty("expression")]
        public Expression Expression { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }
    }
}