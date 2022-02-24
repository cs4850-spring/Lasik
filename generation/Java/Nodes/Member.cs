using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Converters;

namespace Generation.Java.Nodes
{
    [JsonConverter(typeof(MemberConverter))]
    public abstract class Member : Node
    {
        [JsonPropertyName("modifiers")] public List<Modifier> Modifiers { get; set; }

        [JsonPropertyName("annotations")] public List<object> Annotations { get; set; }
    }
}