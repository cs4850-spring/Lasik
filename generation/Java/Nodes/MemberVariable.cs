using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Generation.Java.Nodes
{
    public class MemberVariable : Member
    {
        [JsonPropertyName("variables")] public List<Variable> Variables { get; set; }
    }
}