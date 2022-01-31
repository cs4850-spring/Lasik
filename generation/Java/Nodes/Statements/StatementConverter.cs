using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace generation.Java.Nodes.Statements
{
    public class StatementConverter : JsonConverter<Statement>
    {
        public override Statement? Read(ref Utf8JsonReader reader, System.Type typeToConvert,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Statement value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}