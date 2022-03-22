using System.Text.Json.Serialization;
using Generation.Java.Converters;

namespace Generation.Java.Nodes.Types
{
    [JsonConverter(typeof(JavaTypeConverter))]
    public abstract class JavaType : Node
    {

        public abstract string Identifier();

    }
}