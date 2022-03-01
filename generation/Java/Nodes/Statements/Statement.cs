using System.Text.Json.Serialization;
using Generation.Java.Converters;

namespace Generation.Java.Nodes.Statements
{
    [JsonConverter(typeof(StatementConverter))]
    public abstract class Statement : Node
    {

    }
}