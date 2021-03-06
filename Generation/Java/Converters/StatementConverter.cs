using System;
using System.Text.Json;
using Generation.Java.Nodes.Members;
using Generation.Java.Nodes.Statements;

namespace Generation.Java.Converters
{
    public class StatementConverter : NodeConverter<Statement>
    {
        public StatementConverter()
        {
            // Required for JSON serialization
        }
        
        public override Statement? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var kind = ReadKind(ref reader);
            Statement? statement = kind switch
            {
                "com.github.javaparser.ast.stmt.ExpressionStmt" =>
                    JsonSerializer.Deserialize<ExpressionStatement>(ref reader, options),
                "com.github.javaparser.ast.stmt.BlockStmt" =>
                    JsonSerializer.Deserialize<BlockStatement>(ref reader, options),
                "com.github.javaparser.ast.stmt.IfStmt" =>
                    JsonSerializer.Deserialize<IfStatement>(ref reader, options),
                "com.github.javaparser.ast.stmt.ForStmt" =>
                    JsonSerializer.Deserialize<ForStatement>(ref reader, options),
                "com.github.javaparser.ast.stmt.ForEachStmt" =>
                    JsonSerializer.Deserialize<ForEachStatement>(ref reader, options),
                "com.github.javaparser.ast.stmt.ReturnStmt" =>
                    JsonSerializer.Deserialize<ReturnStatement>(ref reader, options),
                "com.github.javaparser.ast.stmt.ThrowStmt" =>
                    JsonSerializer.Deserialize<ThrowStatement>(ref reader, options),
                "com.github.javaparser.ast.stmt.TryStmt" =>
                    JsonSerializer.Deserialize<TryStatement>(ref reader, options),
                "com.github.javaparser.ast.stmt.ExplicitConstructorInvocationStmt" =>
                    JsonSerializer.Deserialize<ExplicitConstructorInvocationStatement>(ref reader, options),
                "com.github.javaparser.ast.stmt.WhileStmt" =>
                    JsonSerializer.Deserialize<WhileStatement>(ref reader, options),
                "com.github.javaparser.ast.stmt.BreakStmt" => 
                    JsonSerializer.Deserialize<BreakStatement>(ref reader, options),
                _ => throw new NotImplementedException()
            };

            if (statement == null) return null;
            statement.Kind = kind!;

            return statement;
        }
    }
}