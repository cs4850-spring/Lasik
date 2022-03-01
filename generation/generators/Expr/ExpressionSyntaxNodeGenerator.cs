﻿using System;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Expr
{
    public class ExpressionSyntaxNodeGenerator : ISyntaxNodeGenerator<Expression>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, Expression node)
        {
            return node switch
            {
                LiteralExpression literalExpression => 
                    new LiteralExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, literalExpression),
                BinaryExpression binaryExpression => 
                    new BinaryExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, binaryExpression),
                UnaryExpression unaryExpression =>
                    new UnaryExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, unaryExpression),
                VariableDeclarationExpression variableDeclarationExpression =>
                    new VariableDeclarationExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, variableDeclarationExpression),
                AssignExpression assignExpression =>
                    new AssignmentExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, assignExpression),
                _ => throw new ArgumentOutOfRangeException(nameof(node), node, null)
            };
        }
    }
}