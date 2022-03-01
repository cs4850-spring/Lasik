using System;
using Generation.Java.Nodes.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Stmt
{
    public class StatementSyntaxNodeGenerator : ISyntaxNodeGenerator<Statement>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, Statement node)
        {
            return node switch
            {
                ExpressionStatement expressionStatement => 
                    new ExpressionStatementSyntaxNodeGenerator().Generate(syntaxGenerator, expressionStatement), 
                BlockStatement blockStatement =>
                    new BlockStatementSyntaxNodeGenerator().Generate(syntaxGenerator, blockStatement),
                _ => throw new ArgumentOutOfRangeException(nameof(node), node, null)
            };        
        }
    }
}