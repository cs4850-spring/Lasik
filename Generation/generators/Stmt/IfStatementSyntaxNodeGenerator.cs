using Generation.Generators.Expr;
using Generation.Java.Nodes.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Stmt
{
    public class IfStatementSyntaxNodeGenerator : ISyntaxNodeGenerator<IfStatement>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, IfStatement node)
        {
            var condition = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Condition);
            var elseStatement = new StatementSyntaxNodeGenerator().Generate(syntaxGenerator, node.Else);
            var thenStatement = new StatementSyntaxNodeGenerator().Generate(syntaxGenerator, node.Then) as BlockSyntax;

            return elseStatement is BlockSyntax elseBlock
                ? syntaxGenerator.IfStatement(condition, thenStatement?.Statements, elseBlock?.Statements)
                : syntaxGenerator.IfStatement(condition, thenStatement?.Statements, elseStatement);
        }
    }
}