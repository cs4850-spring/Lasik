using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Entry : Node
    {
        
        [JsonProperty("range")]
        public Range Range { get; set; }

        [JsonProperty("tokenRange")]
        public TokenRange TokenRange { get; set; }

        [JsonProperty("labels")]
        public List<Label> Labels { get; set; }

        [JsonProperty("statements")]
        public List<Statement> Statements { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}