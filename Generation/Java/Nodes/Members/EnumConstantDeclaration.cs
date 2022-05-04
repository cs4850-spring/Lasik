using System.Text.Json.Serialization;
using Generation.Java.Nodes;

namespace Generation.Java.Nodes.Members
{
    public class EnumConstantDeclaration : Node
    {
        [JsonPropertyName("name")] public SimpleName Name { get; set; }
    }
}