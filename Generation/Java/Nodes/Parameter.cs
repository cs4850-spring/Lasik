using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Parameter : Node
    {
        [JsonPropertyName("annotations")] public List<object> Annotations { get; set; }

        [JsonPropertyName("isVarArgs")]
        [JsonConverter(typeof(StringConverter))]
        public bool IsVarArgs { get; set; }

        [JsonPropertyName("modifiers")] public List<Modifier> Modifiers { get; set; }

        [JsonPropertyName("name")] public Name Name { get; set; }

        [JsonPropertyName("type")] public Type Type { get; set; }

        [JsonPropertyName("varArgsAnnotations")]
        public List<object> VarArgsAnnotations { get; set; }
    }
}