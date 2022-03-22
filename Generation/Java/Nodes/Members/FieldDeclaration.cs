using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Members
{
    public class FieldDeclaration : Member
    {
        [JsonPropertyName("modifiers")]
        public List<Modifier> Modifiers { get; set; }
        
        [JsonPropertyName("variables")]
        public List<Variable> Variables { get; set; }
    }
}