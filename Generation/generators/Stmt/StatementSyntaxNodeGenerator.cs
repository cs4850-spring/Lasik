using System;
using Generation.Java.Nodes.Expressions;
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
                IfStatement ifStatement =>
                    new IfStatementSyntaxNodeGenerator().Generate(syntaxGenerator, ifStatement),
                ForStatement forStatement => 
                    new ForStatementSyntaxNodeGenerator().Generate(syntaxGenerator, forStatement),
                ForEachStatement forEachStatement =>
                    new ForEachStatementSyntaxNodeGenerator().Generate(syntaxGenerator, forEachStatement),
                _ => throw new ArgumentOutOfRangeException(nameof(node), node, null)
            };        
        }
    }
}