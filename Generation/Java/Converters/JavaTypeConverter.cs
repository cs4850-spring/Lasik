using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Generation.Java.Nodes.Members;
using Generation.Java.Nodes.Types;

namespace Generation.Java.Converters
{
    public class JavaTypeConverter : NodeConverter<JavaType>
    {
        public JavaTypeConverter()
        {
            // Required for JSON serialization
        }
        
        public override JavaType? Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            var kind = ReadKind(ref reader);
            JavaType? type = kind switch
            {
                "com.github.javaparser.ast.type.ArrayType" => JsonSerializer.Deserialize<ArrayJavaType>(ref reader, options),
                "com.github.javaparser.ast.type.ClassOrInterfaceType" => 
                    JsonSerializer.Deserialize<ClassOrInterfaceJavaType>(ref reader, options),
                "com.github.javaparser.ast.type.PrimitiveType" => JsonSerializer.Deserialize<PrimitiveJavaType>(ref reader, options),
                "com.github.javaparser.ast.type.VarType" => JsonSerializer.Deserialize<VarJavaType>(ref reader, options),
                "com.github.javaparser.ast.type.VoidType" => JsonSerializer.Deserialize<VoidJavaType>(ref reader, options),
                _ => throw new NotImplementedException()
            };

            if (type == null) return null;
            type.Kind = kind!;

            return type;
        }
    }
}