using System;
using System.Linq;
using Generation.Java.Nodes.Expressions;
using Generation.Java.Nodes.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public static class StatementGenerators
    {
        public static SyntaxNode Statement(SyntaxGenerator syntaxGenerator, Statement node)
        {
            return node switch
            {
                ExpressionStatement expressionStatement => ExpressionStatement(syntaxGenerator, expressionStatement), 
                BlockStatement blockStatement => BlockStatement(syntaxGenerator, blockStatement),
                IfStatement ifStatement => If(syntaxGenerator, ifStatement),
                ForStatement forStatement => For(syntaxGenerator, forStatement),
                ForEachStatement forEachStatement => ForEach(syntaxGenerator, forEachStatement),
                _ => throw new ArgumentOutOfRangeException(nameof(node), node, null)
            };        
        }
    
        public static SyntaxNode BlockStatement(SyntaxGenerator syntaxGenerator, BlockStatement node)
        {
            var statements = node?.Statements?.Select(statement => Statement(syntaxGenerator, statement) as StatementSyntax);
            return SyntaxFactory.Block(statements);
        }
    
        public static SyntaxNode ExpressionStatement(SyntaxGenerator syntaxGenerator, ExpressionStatement node)
        {
            var expression = ExpressionGenerators.Expression(syntaxGenerator, node.Expression);
            return syntaxGenerator.ExpressionStatement(expression);
        }
    
        public static SyntaxNode ForEach(SyntaxGenerator syntaxGenerator, ForEachStatement node)
        {
            var variable = node.VariableDeclaration.Variables.First();
            var type = TypeGenerators.Type(syntaxGenerator, variable.JavaType) as TypeSyntax;
            var identifier = variable.SimpleName.Identifier;
            var expression = ExpressionGenerators.Expression(syntaxGenerator, node.Iterable) as ExpressionSyntax;
            var statement = Statement(syntaxGenerator, node.Body) as StatementSyntax;

            return SyntaxFactory.ForEachStatement(type, identifier, expression, statement);
        }
    
        public static SyntaxNode For(SyntaxGenerator syntaxGenerator, ForStatement node)
        {
            var declarations = node?.Initialization?.SelectMany(expression => expression.Variables,
                (expression, variable) => BodyGenerators.Variable(syntaxGenerator, variable) as LocalDeclarationStatementSyntax);
            
            var condition = ExpressionGenerators.Expression(syntaxGenerator, node.Comparison) as ExpressionSyntax;
            var incrementors = node?.Updates?.Select(expression =>
                ExpressionGenerators.Expression(syntaxGenerator, expression) as ExpressionSyntax);
            var body = BlockStatement(syntaxGenerator, node.Body) as BlockSyntax;

            var forStatement = SyntaxFactory.ForStatement(declarations.FirstOrDefault(defaultValue: null)?.Declaration, new SeparatedSyntaxList<ExpressionSyntax>(), condition, SyntaxFactory.SeparatedList(incrementors), body);

            // Note(MICHAEL): C# cannot support multiple initializations like java does.
            // If we have multiple declarations we must break this out into a block
            if (declarations.Count() > 1)
            {
                return SyntaxFactory.Block(declarations.Skip(1)).AddStatements(forStatement);
            }
        
            return forStatement;
        }
    
        public static SyntaxNode If(SyntaxGenerator syntaxGenerator, IfStatement node)
        {
            var condition = ExpressionGenerators.Expression(syntaxGenerator, node.Condition);
            var elseStatement = Statement(syntaxGenerator, node.Else);
            var thenStatement = Statement(syntaxGenerator, node.Then) as BlockSyntax;

            return elseStatement is BlockSyntax elseBlock
                ? syntaxGenerator.IfStatement(condition, thenStatement?.Statements, elseBlock?.Statements)
                : syntaxGenerator.IfStatement(condition, thenStatement?.Statements, elseStatement);
        }
    
        public static SyntaxNode VariableDeclaration(SyntaxGenerator syntaxGenerator, VariableDeclarationExpression node)
        {
            return BodyGenerators.Variable(syntaxGenerator, node.Variables.First());
        }
    
    
    }
}