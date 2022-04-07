using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Generation.Java.Nodes.Statements
{
    public class TryStatement : Statement
    {
        [JsonPropertyName("tryBlock")] public BlockStatement TryBlock { get; set; }
        [JsonPropertyName("catchClauses")] public List<CatchClause> CatchClauses { get; set; }
        [JsonPropertyName("finallyBlock")] public BlockStatement FinallyBlock { get; set; }

        public class CatchClause : Node
        {
            [JsonPropertyName("body")] public BlockStatement Body { get; set; }
            [JsonPropertyName("parameter")] public Parameter Parameter { get; set; }
        }
    }
}