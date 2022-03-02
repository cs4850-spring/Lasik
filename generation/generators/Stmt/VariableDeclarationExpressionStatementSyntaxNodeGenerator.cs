using System.Linq;
using Generation.Generators.Body;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Stmt
{
    public class VariableDeclarationExpressionStatementSyntaxNodeGenerator : ISyntaxNodeGenerator<VariableDeclarationExpression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, VariableDeclarationExpression node)
        {
            return new VariableSyntaxNodeGenerator().Generate(syntaxGenerator, node.Variables.First());
        }
    }
}