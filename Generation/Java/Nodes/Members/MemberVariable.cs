using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace generation.Java.Nodes.Members
{
    public class MemberVariable : Member
    {
        [JsonPropertyName("variables")] public List<Variable> Variables { get; set; }
    }
}