using Generation.Generators.Body;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Expr
{
    public class FieldAccessExpressionSyntaxNodeGenerator : ISyntaxNodeGenerator<FieldAccessExpression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, FieldAccessExpression node)
        {
            var expression = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Scope);
            var memberName = new NameSyntaxNodeGenerator().Generate(syntaxGenerator, node.Name);
            return syntaxGenerator.MemberAccessExpression(expression, memberName);
        }
    }
}