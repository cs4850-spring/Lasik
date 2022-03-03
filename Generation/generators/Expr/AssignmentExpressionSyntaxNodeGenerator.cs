using System;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Expr
{
    public class AssignmentExpressionSyntaxNodeGenerator : ISyntaxNodeGenerator<AssignExpression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, AssignExpression node)
        {
            var name = new NameExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Target);
            var value = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Value);
            var (syntaxKind, syntaxToken) = node.Operator switch
            {
                "PLUS" => (SyntaxKind.AddAssignmentExpression, SyntaxKind.PlusEqualsToken),
                "MINUS" => (SyntaxKind.SubtractAssignmentExpression, SyntaxKind.MinusEqualsToken),
                "MULTIPLY" => (SyntaxKind.MultiplyAssignmentExpression, SyntaxKind.AsteriskEqualsToken),
                "DIVIDE" => (SyntaxKind.DivideAssignmentExpression, SyntaxKind.SlashEqualsToken),
                "REMAINDER" => (SyntaxKind.ModuloAssignmentExpression, SyntaxKind.PercentEqualsToken),
                "LEFT_SHIFT" => (SyntaxKind.LeftShiftAssignmentExpression, SyntaxKind.LessThanLessThanEqualsToken),
                "SIGNED_RIGHT_SHIFT" => (SyntaxKind.RightShiftAssignmentExpression, SyntaxKind.GreaterThanGreaterThanEqualsToken),
                "XOR" => (SyntaxKind.ExclusiveOrAssignmentExpression, SyntaxKind.CaretEqualsToken),
                "BINARY_OR" => (SyntaxKind.OrAssignmentExpression, SyntaxKind.BarEqualsToken),
                "BINARY_AND" => (SyntaxKind.AndAssignmentExpression, SyntaxKind.AmpersandEqualsToken),
                "ASSIGN" => (SyntaxKind.SimpleAssignmentExpression, SyntaxKind.EqualsToken),
                _ => throw new ArgumentOutOfRangeException(),
            };

            
            return SyntaxFactory.AssignmentExpression(syntaxKind, (ExpressionSyntax) name,
                SyntaxFactory.Token(syntaxToken), (ExpressionSyntax) value);
        }
    }
}