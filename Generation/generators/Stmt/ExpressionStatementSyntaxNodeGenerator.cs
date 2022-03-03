using Generation.Generators.Expr;
using Generation.Java.Nodes.Expressions;
using Generation.Java.Nodes.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Stmt
{
    public class ExpressionStatementSyntaxNodeGenerator : ISyntaxNodeGenerator<ExpressionStatement>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, ExpressionStatement node)
        {
            var expression = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Expression);
            return syntaxGenerator.ExpressionStatement(expression);
        }
    }
}