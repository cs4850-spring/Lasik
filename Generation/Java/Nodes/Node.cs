using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Generation.Java.Nodes
{
    /**
     * A Node represents a specific meta-object (Type, Member, Variable, etc) on the java AST.
     * Nodes may have children, which can be enumerated.
     */
    public abstract class Node
    {
        [JsonPropertyName("!")] public string Class { get; set; }
        public TextRange Range { get; set; }
        public TokenRange TokenRange { get; set; }
    }
}