using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Nodes.Statements;
using Generation.Java.Nodes.Types;

namespace Generation.Java.Nodes.Members
{
    public class ConstructorDeclaration : Member
    {
        [JsonPropertyName("body")] public BlockStatement Body { get; set; }
        
        [JsonPropertyName("name")] public SimpleName SimpleName { get; set; }

        [JsonPropertyName("parameters")] public List<Parameter> Parameters { get; set; }

        [JsonPropertyName("thrownExceptions")] public List<JavaType> ThrownExceptions { get; set; }

        [JsonPropertyName("typeParameters")] public List<object> TypeParameters { get; set; }
    }
}