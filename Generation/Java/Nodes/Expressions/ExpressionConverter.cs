using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace generation.Java.Nodes.Expressions
{
    public class ExpressionConverter : JsonConverter<Expression>
    {
        public override Expression? Read(ref Utf8JsonReader reader, System.Type typeToConvert,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Expression value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}