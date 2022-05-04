using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Nodes;

namespace Generation.Java.Nodes.Members
{
    public class EnumDeclaration : Member
    {
        [JsonPropertyName("entries")] public List<EnumConstantDeclaration> Entries { get; set; }
        [JsonPropertyName("name")] public SimpleName Name { get; set; }
    }
}