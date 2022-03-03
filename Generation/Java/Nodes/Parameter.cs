using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Converters;
using Generation.Java.Nodes.Types;

namespace Generation.Java.Nodes
{
    public class Parameter : Node
    {
        [JsonPropertyName("annotations")] public List<object> Annotations { get; set; }

        [JsonPropertyName("isVarArgs")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsVarArgs { get; set; }

        [JsonPropertyName("modifiers")] public List<Modifier> Modifiers { get; set; }

        [JsonPropertyName("name")] public SimpleName SimpleName { get; set; }

        [JsonPropertyName("type")] public JavaType JavaType { get; set; }

        [JsonPropertyName("varArgsAnnotations")]
        public List<object> VarArgsAnnotations { get; set; }
    }
}