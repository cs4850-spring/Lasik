using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Generation.Java.Nodes;

namespace Generation.Java.Converters
{
    public class MemberConverter : JsonConverter<Member>
    {
        public override Member? Read(ref Utf8JsonReader reader, System.Type typeToConvert,
            JsonSerializerOptions options)
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
            Console.WriteLine(kind);
            Member? member = kind switch
            {
                "com.github.javaparser.ast.body.FieldDeclaration" => 
                    JsonSerializer.Deserialize<MemberVariable>(ref reader, options),
                "com.github.javaparser.ast.body.MethodDeclaration" => 
                    JsonSerializer.Deserialize<MemberMethod>(ref reader, options),
                _ => throw new NotImplementedException()
            };

            if (member == null) return null;
            member.Kind = kind!;

            return member;
        }

        public override void Write(Utf8JsonWriter writer, Member value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}