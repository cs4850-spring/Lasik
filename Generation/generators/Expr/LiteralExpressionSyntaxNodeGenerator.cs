using System;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Expr
{
    public class LiteralExpressionSyntaxNodeGenerator : ISyntaxNodeGenerator<LiteralExpression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, LiteralExpression node)
        {
            if (node.Kind is "com.github.javaparser.ast.expr.BooleanLiteralExpr")
            {
                var value = bool.Parse(node.Value);
                return value ? syntaxGenerator.TrueLiteralExpression() : syntaxGenerator.FalseLiteralExpression();
            }

            if (node.Kind is "com.github.javaparser.ast.expr.NullLiteralExpr")
            {
                return syntaxGenerator.NullLiteralExpression();
            }
            
            var literalKind = node.Kind switch
            {
                "com.github.javaparser.ast.expr.IntegerLiteralExpr" => SyntaxKind.NumericLiteralExpression,
                "com.github.javaparser.ast.expr.CharLiteralExpr" => SyntaxKind.CharacterLiteralExpression,
                "com.github.javaparser.ast.expr.StringLiteralExpr" => SyntaxKind.StringLiteralExpression,
                "com.github.javaparser.ast.expr.DoubleLiteralExpr" => SyntaxKind.NumericLiteralExpression,
                "com.github.javaparser.ast.expr.LongLiteralExpr" => SyntaxKind.NumericLiteralExpression,
                _ => throw new ArgumentOutOfRangeException()
            };

            var literalToken = new SyntaxToken();
            try
            {
                literalToken = node.Kind switch
                {
                    "com.github.javaparser.ast.expr.IntegerLiteralExpr" => SyntaxFactory.Literal(int.Parse(node.Value)),
                    "com.github.javaparser.ast.expr.CharLiteralExpr" => SyntaxFactory.Literal(node.Value[0]),
                    "com.github.javaparser.ast.expr.StringLiteralExpr" => SyntaxFactory.Literal(node.Value),
                    "com.github.javaparser.ast.expr.DoubleLiteralExpr" => SyntaxFactory.Literal(
                        double.Parse(node.Value.TrimEnd('f', 'F'))),
                    "com.github.javaparser.ast.expr.LongLiteralExpr" => SyntaxFactory.Literal(long.Parse(node.Value.TrimEnd('l', 'L'))),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            catch (Exception ex)
            {
                literalToken = SyntaxFactory.Literal(node.Value);
            }
            
            return SyntaxFactory.LiteralExpression(literalKind, literalToken);
        }
    }
}