using System.ComponentModel;
using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Import
    {
        [JsonPropertyName("isAsterisk")]
        [JsonConverter(typeof(StringConverter))]
        public bool IsAsterisk { get; set; }

        [JsonPropertyName("isStatic")]
        [JsonConverter(typeof(StringConverter))]
        public bool IsStatic { get; set; }

        [JsonPropertyName("name")] public Name Name { get; set; }
    }
}