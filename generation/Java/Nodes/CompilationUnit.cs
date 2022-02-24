using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Nodes.Types;

namespace Generation.Java.Nodes
{
    public class CompilationUnit : Node
    {
        [JsonPropertyName("imports")] public List<Import> Imports { get; set; }

        [JsonPropertyName("types")] public List<ClassOrInterfaceJavaType> Types { get; set; }
    }
}