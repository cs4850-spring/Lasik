using System.Linq;
using Generation.Java.Nodes.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Stmt
{
    public class BlockStatementSyntaxNodeGenerator : ISyntaxNodeGenerator<BlockStatement>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, BlockStatement node)
        {
            var statements = node?.Statements?.Select(statement =>
                new StatementSyntaxNodeGenerator().Generate(syntaxGenerator, statement) as StatementSyntax);
            return SyntaxFactory.Block(statements);
        }
    }
}