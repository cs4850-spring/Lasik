using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace generation.Java.Nodes
{
    public class Type : Node
    {
        [JsonPropertyName("extendedTypes")] public List<object> ExtendedTypes { get; set; }

        [JsonPropertyName("implementedTypes")] public List<object> ImplementedTypes { get; set; }

        [JsonPropertyName("isInterface")]
        [JsonConverter(typeof(StringConverter))]
        public bool IsInterface { get; set; }

        [JsonPropertyName("typeParameters")] public List<object> TypeParameters { get; set; }

        [JsonPropertyName("members")] public List<Member> Members { get; set; }

        [JsonPropertyName("modifiers")] public List<Modifier> Modifiers { get; set; }

        [JsonPropertyName("name")] public Name Name { get; set; }

        [JsonPropertyName("annotations")] public List<object> Annotations { get; set; }
    }
}