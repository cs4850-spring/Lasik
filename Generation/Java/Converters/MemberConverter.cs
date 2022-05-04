using System;
using System.Text.Json;
using Generation.Java.Nodes.Members;
using Generation.Java.Nodes.Types;

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
                    JsonSerializer.Deserialize<FieldDeclaration>(ref reader, options),
                "com.github.javaparser.ast.body.MethodDeclaration" =>
                    JsonSerializer.Deserialize<MethodDeclaration>(ref reader, options),
                "com.github.javaparser.ast.body.ClassOrInterfaceDeclaration" =>
                    JsonSerializer.Deserialize<ClassOrInterfaceDeclaration>(ref reader, options),
                "com.github.javaparser.ast.body.ConstructorDeclaration" =>
                    JsonSerializer.Deserialize<ConstructorDeclaration>(ref reader, options),
                "com.github.javaparser.ast.body.EnumDeclaration" =>
                    JsonSerializer.Deserialize<EnumDeclaration>(ref reader, options),
                _ => throw new NotImplementedException()
            };

            if (member == null) return null;
            member.Kind = kind!;

            return member;
        }
    }
}