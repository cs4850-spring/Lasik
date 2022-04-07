using System.Collections.Generic;
using System.Text.Json.Serialization;
using Generation.Java.Nodes.Types;

namespace Generation.Java.Nodes.Expressions
{
    public class ArrayCreationExpression : Expression
    {
        [JsonPropertyName("elementType")] public JavaType Type { get; set; }
        [JsonPropertyName("levels")] public List<ArrayCreationLevels> Levels { get; set; }

        public class ArrayCreationLevels : Node
        {
            [JsonPropertyName("dimension")] public Expression Dimension { get; set;  }
        }
    }
}