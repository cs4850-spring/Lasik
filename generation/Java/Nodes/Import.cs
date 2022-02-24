using System.ComponentModel;
using System.Text.Json.Serialization;
using Generation.Java.Converters;

namespace Generation.Java.Nodes
{
    public class Import : Node
    {
        [JsonPropertyName("isAsterisk")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsAsterisk { get; set; }

        [JsonPropertyName("isStatic")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsStatic { get; set; }

        [JsonPropertyName("name")]
        public SimpleName Name { get; set; }
    }
}