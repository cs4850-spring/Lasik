using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Nodes.Types;

namespace Generation.Java.Nodes.Members
{
    public class MemberMethod : Member
    {
        [JsonPropertyName("body")] public Body Body { get; set; }

        [JsonPropertyName("type")] public JavaType JavaType { get; set; }

        [JsonPropertyName("name")] public SimpleName SimpleName { get; set; }

        [JsonPropertyName("parameters")] public List<Parameter> Parameters { get; set; }

        [JsonPropertyName("thrownExceptions")] public List<object> ThrownExceptions { get; set; }

        [JsonPropertyName("typeParameters")] public List<object> TypeParameters { get; set; }
    }
}