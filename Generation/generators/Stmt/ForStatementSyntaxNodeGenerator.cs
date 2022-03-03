using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Generation.Generators.Body;
using Generation.Generators.Expr;
using Generation.Java.Nodes.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Stmt
{
    public class ForStatementSyntaxNodeGenerator : ISyntaxNodeGenerator<ForStatement>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, ForStatement node)
        {
            var declarations = node?.Initialization?.SelectMany(expression => expression.Variables,
                (expression, variable) => new VariableSyntaxNodeGenerator().Generate(syntaxGenerator, variable) as LocalDeclarationStatementSyntax);
            
            var condition = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, node.Comparison) as ExpressionSyntax;
            var incrementors = node?.Updates?.Select(expression =>
                new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, expression) as ExpressionSyntax);
            var body = new BlockStatementSyntaxNodeGenerator().Generate(syntaxGenerator, node.Body) as BlockSyntax;

            var forStatement = SyntaxFactory.ForStatement(declarations.FirstOrDefault(defaultValue: null)?.Declaration, new SeparatedSyntaxList<ExpressionSyntax>(), condition, SyntaxFactory.SeparatedList(incrementors), body);

            // Note(MICHAEL): C# cannot support multiple initializations like java does.
            // If we have multiple declarations we must break this out into a block
            if (declarations.Count() > 1)
            {
                return SyntaxFactory.Block(declarations.Skip(1)).AddStatements(forStatement);
            }

            return forStatement;
        }
        
    }
}