using System;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Expr
{
    public class BinaryExpressionSyntaxNodeGenerator : ISyntaxNodeGenerator<BinaryExpression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, BinaryExpression node)
        {
            var left = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Left);
            var right = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Right);

            var (syntaxKind, syntaxToken) = node.Operator switch
            {
                "LESS" => (SyntaxKind.LessThanExpression, SyntaxKind.LessThanToken),
                "LESS_EQUALS" => (SyntaxKind.LessThanOrEqualExpression, SyntaxKind.LessThanEqualsToken),
                "GREATER" => (SyntaxKind.GreaterThanExpression, SyntaxKind.GreaterThanToken),
                "GREATER_EQUALS" => (SyntaxKind.GreaterThanOrEqualExpression, SyntaxKind.GreaterThanEqualsToken),
                "EQUALS" => (SyntaxKind.EqualsExpression, SyntaxKind.EqualsEqualsToken),
                "NOT_EQUALS" => (SyntaxKind.NotEqualsExpression, SyntaxKind.ExclamationEqualsToken),
                "AND" => (SyntaxKind.LogicalAndExpression, SyntaxKind.AmpersandAmpersandToken),
                "OR" => (SyntaxKind.LogicalOrExpression, SyntaxKind.BarBarToken),

                "LEFT_SHIFT" => (SyntaxKind.LeftShiftExpression, SyntaxKind.LessThanLessThanToken),
                "SIGNED_RIGHT_SHIFT" => (SyntaxKind.RightShiftExpression, SyntaxKind.GreaterThanGreaterThanToken),
                "XOR" => (SyntaxKind.ExclusiveOrExpression, SyntaxKind.CaretToken),
                "BINARY_OR" => (SyntaxKind.BitwiseOrExpression, SyntaxKind.BarToken),
                "BINARY_AND" => (SyntaxKind.BitwiseAndExpression, SyntaxKind.AmpersandToken),
                
                "PLUS" => (SyntaxKind.AddExpression, SyntaxKind.PlusToken),
                "MINUS" => (SyntaxKind.SubtractExpression, SyntaxKind.MinusToken),
                "MULTIPLY" => (SyntaxKind.MultiplyExpression, SyntaxKind.AsteriskToken),
                "DIVIDE" => (SyntaxKind.DivideExpression, SyntaxKind.SlashToken),
                "REMAINDER" => (SyntaxKind.ModuloExpression, SyntaxKind.PercentToken),
                
                _ => throw new ArgumentOutOfRangeException()
            };
            
            return SyntaxFactory.BinaryExpression(syntaxKind, (ExpressionSyntax) left, SyntaxFactory.Token(syntaxToken), (ExpressionSyntax) right);
        }
    }
}