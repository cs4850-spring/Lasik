using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Generation.Java.Nodes.Expressions;
using Expression = Generation.Java.Nodes.Expressions.Expression;

namespace Generation.Java.Converters
{
    public class ExpressionConverter : NodeConverter<Expression>
    {
        public ExpressionConverter()
        {
            // Required for JSON serialization
        }

        public override Expression? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var kind = ReadKind(ref reader);

            Expression? expression = kind switch
            {
                "com.github.javaparser.ast.expr.IntegerLiteralExpr" => 
                    JsonSerializer.Deserialize<LiteralExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.LongLiteralExpr" => 
                    JsonSerializer.Deserialize<LiteralExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.DoubleLiteralExpr" => 
                    JsonSerializer.Deserialize<LiteralExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.BooleanLiteralExpr" => 
                    JsonSerializer.Deserialize<LiteralExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.CharLiteralExpr" => 
                    JsonSerializer.Deserialize<LiteralExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.StringLiteralExpr" => 
                    JsonSerializer.Deserialize<LiteralExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.NullLiteralExpr" => 
                    JsonSerializer.Deserialize<LiteralExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.BinaryExpr" =>
                    JsonSerializer.Deserialize<BinaryExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.UnaryExpr" =>
                    JsonSerializer.Deserialize<UnaryExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.AssignExpr" =>
                    JsonSerializer.Deserialize<AssignExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.NameExpr" =>
                    JsonSerializer.Deserialize<NameExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.VariableDeclarationExpr" =>
                    JsonSerializer.Deserialize<VariableDeclarationExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.MethodCallExpr" =>
                    JsonSerializer.Deserialize<MethodCallExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.FieldAccessExpr" =>
                    JsonSerializer.Deserialize<FieldAccessExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.ThisExpr" => 
                    JsonSerializer.Deserialize<ThisExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.CastExpr" => 
                    JsonSerializer.Deserialize<CastExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.EnclosedExpr" =>
                    JsonSerializer.Deserialize<EnclosedExpression>(ref reader, options),
                "com.github.javaparser.ast.expr.ArrayCreationExpr" =>
                    JsonSerializer.Deserialize<ArrayCreationExpression>(ref reader, options),
                _ => throw new ArgumentOutOfRangeException()
            };
            if (expression == null) return null;
            expression.Kind = kind!;

            return expression;
            
        }
    }
}