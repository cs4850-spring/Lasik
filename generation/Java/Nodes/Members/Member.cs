using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace generation.Java.Nodes.Members
{
    [JsonConverter(typeof(MemberConverter))]
    public abstract class Member : Node
    {
        [JsonPropertyName("modifiers")] public List<Modifier> Modifiers { get; set; }

        [JsonPropertyName("annotations")] public List<object> Annotations { get; set; }
    }
}