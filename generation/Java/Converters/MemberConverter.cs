using System;
using System.Text.Json;
using Generation.Java.Nodes.Members;

namespace Generation.Java.Converters
{
    public class MemberConverter : NodeConverter<Member>
    {
        public MemberConverter()
        {
            // Required for JSON serialization
        }
        
        public override Member? Read(ref Utf8JsonReader reader, System.Type typeToConvert,
            JsonSerializerOptions options)
        {
            var kind = ReadKind(ref reader);
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
    }
}