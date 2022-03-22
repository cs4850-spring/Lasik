using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace generation.Java.Nodes.Members
{
    public class MemberMethod : Member
    {
        [JsonPropertyName("body")] public Body Body { get; set; }

        [JsonPropertyName("type")] public Type Type { get; set; }

        [JsonPropertyName("name")] public Name Name { get; set; }

        [JsonPropertyName("parameters")] public List<Parameter> Parameters { get; set; }

        [JsonPropertyName("thrownExceptions")] public List<object> ThrownExceptions { get; set; }

        [JsonPropertyName("typeParameters")] public List<object> TypeParameters { get; set; }
    }
}