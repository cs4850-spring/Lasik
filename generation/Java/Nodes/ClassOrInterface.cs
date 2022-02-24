using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Converters;

namespace Generation.Java.Nodes
{
    // NOTE(MICHAEL): I Really hate this name, but its what the kind is in java and I cant think of a better one right noe
    public class ClassOrInterface : Node
    {
        [JsonPropertyName("extendedTypes")] public List<object> ExtendedTypes { get; set; }

        [JsonPropertyName("implementedTypes")] public List<object> ImplementedTypes { get; set; }

        [JsonPropertyName("isInterface")]
        [JsonConverter(typeof(BoolConverter))]
        public bool IsInterface { get; set; }

        [JsonPropertyName("typeParameters")] public List<object> TypeParameters { get; set; }

        [JsonPropertyName("members")] public List<Member> Members { get; set; }

        [JsonPropertyName("modifiers")] public List<Modifier> Modifiers { get; set; }

        [JsonPropertyName("name")] public SimpleName SimpleName { get; set; }

        [JsonPropertyName("annotations")] public List<object> Annotations { get; set; }
    }
}