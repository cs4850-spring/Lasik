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
                ReturnStatement returnStatement => Return(syntaxGenerator, returnStatement),
                ThrowStatement throwStatement => Throw(syntaxGenerator, throwStatement),
                TryStatement tryStatement => Try(syntaxGenerator, tryStatement),
                ExplicitConstructorInvocationStatement explicitConstructorInvocationStatement => SyntaxFactory.EmptyStatement(),
                WhileStatement whileStatement => While(syntaxGenerator, whileStatement),
                BreakStatement breakStatement => Break(syntaxGenerator, breakStatement),
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
            var elseStatement = node.Else != null
                ? Statement(syntaxGenerator, node.Else)
                : null;
            var thenStatement = Statement(syntaxGenerator, node.Then) as BlockSyntax;

            if (elseStatement is BlockSyntax elseBlock)
            {
                return syntaxGenerator.IfStatement(condition, thenStatement?.Statements, elseBlock?.Statements);
            }
            
            var elseClause = elseStatement != null 
                ? SyntaxFactory.ElseClause(elseStatement as StatementSyntax)
                : null;
            return SyntaxFactory.IfStatement(condition as ExpressionSyntax, thenStatement, elseClause);
        }
    
        public static SyntaxNode VariableDeclaration(SyntaxGenerator syntaxGenerator, VariableDeclarationExpression node)
        {
            return BodyGenerators.Variable(syntaxGenerator, node.Variables.First());
        }

        public static SyntaxNode Return(SyntaxGenerator syntaxGenerator, ReturnStatement node)
        {
            if (node.Expression != null)
            {
                var expression = ExpressionGenerators.Expression(syntaxGenerator, node.Expression);
                return syntaxGenerator.ReturnStatement(expression);
            }

            return syntaxGenerator.ReturnStatement();
        }
    
        public static SyntaxNode Throw(SyntaxGenerator syntaxGenerator, ThrowStatement node)
        {
            var expression = ExpressionGenerators.Expression(syntaxGenerator, node.Expression);
            return syntaxGenerator.ThrowStatement(expression);
        }

        public static SyntaxNode Try(SyntaxGenerator syntaxGenerator, TryStatement node)
        {
            var tryBlock = BlockStatement(syntaxGenerator, node.TryBlock) as BlockSyntax;
            var finallyBlock = BlockStatement(syntaxGenerator, node.FinallyBlock) as BlockSyntax;

            var catchClauses = node?.CatchClauses?.Select(clause =>
            {
                var parameter = BodyGenerators.Parameter(syntaxGenerator, clause.Parameter) as ParameterSyntax;
                var clauseBody = BlockStatement(syntaxGenerator, clause.Body) as BlockSyntax;
                return syntaxGenerator.CatchClause(parameter.Type, parameter.Identifier.Text, clauseBody?.Statements);
            });
            
            return syntaxGenerator.TryCatchStatement(tryBlock?.Statements, catchClauses, finallyBlock?.Statements);
        }

        public static SyntaxNode While(SyntaxGenerator syntaxGenerator, WhileStatement node)
        {
            var condition = ExpressionGenerators.Expression(syntaxGenerator, node.Conditional);
            var body = Statement(syntaxGenerator, node.Body);

            var statements = new SyntaxList<SyntaxNode>();
            statements = body switch
            {
                BlockSyntax block => block.Statements,
                ExpressionStatementSyntax expressionStatement => statements.Add(expressionStatement),
                _ => statements
            };

            return syntaxGenerator.WhileStatement(condition, statements);
        }

        public static SyntaxNode Break(SyntaxGenerator syntaxGenerator, BreakStatement node)
        {
            return SyntaxFactory.BreakStatement();
        }
    }
}