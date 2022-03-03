using System.Text.Json;
using System.Text.Json.Serialization;
using Generation.Java.Nodes;

namespace Generation.Java.Converters
{
    public abstract class NodeConverter<TNode> : JsonConverter<TNode> where TNode : Node
    {
        public NodeConverter()
        {
            // Required for JSON serialization
        }
        
        public override void Write(Utf8JsonWriter writer, TNode value, JsonSerializerOptions options)
        {
            // NOTE(MICHAEL): We never plan on writing these nodes out
            throw new System.NotImplementedException();
        }

        protected string ReadKind(ref Utf8JsonReader reader)
        {
            var head = reader;
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            if (!reader.Read()
                || reader.TokenType != JsonTokenType.PropertyName
                || reader.GetString() != "!")
            {
                throw new JsonException();
            }

            if (!reader.Read() || reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }
            
            var kind = reader.GetString();

            reader = head; // reset back to start position

            return kind!;
        }
    }
}