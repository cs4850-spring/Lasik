using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Nodes.Types;

namespace Generation.Java.Nodes
{
    public class TypeParameter : Node
    {
        [JsonPropertyName("name")] public SimpleName Name { get; set; }
        [JsonPropertyName("typeBound")] public List<JavaType> TypeBounds { get; set; }
    }
}