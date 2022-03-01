using System;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Expr
{
    public class UnaryExpressionSyntaxNodeGenerator : ISyntaxNodeGenerator<UnaryExpression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, UnaryExpression node)
        {
            var expression = (ExpressionSyntax) new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Expression);

            var (syntaxKind, syntaxToken) = node.Operator switch
            {
                "BITWISE_COMPLEMENT" => (SyntaxKind.BitwiseNotExpression, SyntaxKind.CaretToken),
                "PREFIX_INCREMENT" => (SyntaxKind.PreIncrementExpression, SyntaxKind.PlusPlusToken),
                "PREFIX_DECREMENT" => (SyntaxKind.PreDecrementExpression, SyntaxKind.MinusMinusToken),
                "POSTFIX_INCREMENT" => (SyntaxKind.PostIncrementExpression, SyntaxKind.PlusPlusToken),
                "POSTFIX_DECREMENT" => (SyntaxKind.PostDecrementExpression, SyntaxKind.MinusMinusToken),
                _ => throw new ArgumentOutOfRangeException()
            };

            var operandToken = SyntaxFactory.Token(syntaxToken);
            return node.Operator switch
            {
                "BITWISE_COMPLEMENT" => SyntaxFactory.PrefixUnaryExpression(syntaxKind, operandToken, expression),
                "PREFIX_INCREMENT" => SyntaxFactory.PrefixUnaryExpression(syntaxKind, operandToken, expression),
                "PREFIX_DECREMENT" => SyntaxFactory.PrefixUnaryExpression(syntaxKind, operandToken, expression),
                "POSTFIX_INCREMENT" => SyntaxFactory.PostfixUnaryExpression(syntaxKind, expression, operandToken),
                "POSTFIX_DECREMENT" => SyntaxFactory.PostfixUnaryExpression(syntaxKind, expression, operandToken),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}