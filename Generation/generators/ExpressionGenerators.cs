using System;
using System.Collections.Generic;
using System.Linq;
using Generation.Java.Nodes;
using Generation.Java.Nodes.Expressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public static class ExpressionGenerators
    {
        public static SyntaxNode Expression(SyntaxGenerator syntaxGenerator, Expression node)
        {
            return node switch
            {
                LiteralExpression literalExpression => Literal(syntaxGenerator, literalExpression),
                BinaryExpression binaryExpression => Binary(syntaxGenerator, binaryExpression),
                UnaryExpression unaryExpression => Unary(syntaxGenerator, unaryExpression),
                VariableDeclarationExpression variableDeclarationExpression => VariableDeclaration(syntaxGenerator, variableDeclarationExpression),
                AssignExpression assignExpression => Assignment(syntaxGenerator, assignExpression),
                NameExpression nameExpression => Name(syntaxGenerator, nameExpression),
                MethodCallExpression methodCallExpression => MethodCall(syntaxGenerator, methodCallExpression),
                FieldAccessExpression fieldAccessExpression => FieldAccess(syntaxGenerator, fieldAccessExpression),
                ThisExpression thisExpression => This(syntaxGenerator,  thisExpression),
                CastExpression castExpression => Cast(syntaxGenerator, castExpression),
                EnclosedExpression enclosedExpression => Enclosed(syntaxGenerator, enclosedExpression),
                ArrayCreationExpression arrayCreationExpression => ArrayCreation(syntaxGenerator, arrayCreationExpression),
                ArrayAccessExpression arrayAccessExpression => ArrayAccess(syntaxGenerator, arrayAccessExpression),
                ArrayInitializerExpression arrayInitializerExpression => ArrayInitializer(syntaxGenerator, arrayInitializerExpression),
                ObjectCreationExpression objectCreationExpression => ObjectCreation(syntaxGenerator, objectCreationExpression),
                SuperExpression superExpression => Super(syntaxGenerator, superExpression),
                _ => throw new ArgumentOutOfRangeException(nameof(node), node, null)
            };
        }

        public static SyntaxNode Assignment(SyntaxGenerator syntaxGenerator, AssignExpression node)
        {
            var name = Expression(syntaxGenerator, node.Target);
            var value = Expression(syntaxGenerator, node.Value);
            var (syntaxKind, syntaxToken) = node.Operator switch
            {
                "PLUS" => (SyntaxKind.AddAssignmentExpression, SyntaxKind.PlusEqualsToken),
                "MINUS" => (SyntaxKind.SubtractAssignmentExpression, SyntaxKind.MinusEqualsToken),
                "MULTIPLY" => (SyntaxKind.MultiplyAssignmentExpression, SyntaxKind.AsteriskEqualsToken),
                "DIVIDE" => (SyntaxKind.DivideAssignmentExpression, SyntaxKind.SlashEqualsToken),
                "REMAINDER" => (SyntaxKind.ModuloAssignmentExpression, SyntaxKind.PercentEqualsToken),
                "LEFT_SHIFT" => (SyntaxKind.LeftShiftAssignmentExpression, SyntaxKind.LessThanLessThanEqualsToken),
                "SIGNED_RIGHT_SHIFT" => (SyntaxKind.RightShiftAssignmentExpression, SyntaxKind.GreaterThanGreaterThanEqualsToken),
                "XOR" => (SyntaxKind.ExclusiveOrAssignmentExpression, SyntaxKind.CaretEqualsToken),
                "BINARY_OR" => (SyntaxKind.OrAssignmentExpression, SyntaxKind.BarEqualsToken),
                "BINARY_AND" => (SyntaxKind.AndAssignmentExpression, SyntaxKind.AmpersandEqualsToken),
                "ASSIGN" => (SyntaxKind.SimpleAssignmentExpression, SyntaxKind.EqualsToken),
                _ => throw new ArgumentOutOfRangeException(),
            };

            
            return SyntaxFactory.AssignmentExpression(syntaxKind, (ExpressionSyntax) name,
                SyntaxFactory.Token(syntaxToken), (ExpressionSyntax) value);
        }
    
        public static SyntaxNode Binary(SyntaxGenerator syntaxGenerator, BinaryExpression node)
        {
            var left = Expression(syntaxGenerator, node.Left);
            var right = Expression(syntaxGenerator, node.Right);
            var (syntaxKind, syntaxToken) = node.Operator switch
            {
                "LESS" => (SyntaxKind.LessThanExpression, SyntaxKind.LessThanToken),
                "LESS_EQUALS" => (SyntaxKind.LessThanOrEqualExpression, SyntaxKind.LessThanEqualsToken),
                "GREATER" => (SyntaxKind.GreaterThanExpression, SyntaxKind.GreaterThanToken),
                "GREATER_EQUALS" => (SyntaxKind.GreaterThanOrEqualExpression, SyntaxKind.GreaterThanEqualsToken),
                "EQUALS" => (SyntaxKind.EqualsExpression, SyntaxKind.EqualsEqualsToken),
                "NOT_EQUALS" => (SyntaxKind.NotEqualsExpression, SyntaxKind.ExclamationEqualsToken),
                "AND" => (SyntaxKind.LogicalAndExpression, SyntaxKind.AmpersandAmpersandToken),
                "OR" => (SyntaxKind.LogicalOrExpression, SyntaxKind.BarBarToken),

                "LEFT_SHIFT" => (SyntaxKind.LeftShiftExpression, SyntaxKind.LessThanLessThanToken),
                "SIGNED_RIGHT_SHIFT" => (SyntaxKind.RightShiftExpression, SyntaxKind.GreaterThanGreaterThanToken),
                "XOR" => (SyntaxKind.ExclusiveOrExpression, SyntaxKind.CaretToken),
                "BINARY_OR" => (SyntaxKind.BitwiseOrExpression, SyntaxKind.BarToken),
                "BINARY_AND" => (SyntaxKind.BitwiseAndExpression, SyntaxKind.AmpersandToken),
                
                "PLUS" => (SyntaxKind.AddExpression, SyntaxKind.PlusToken),
                "MINUS" => (SyntaxKind.SubtractExpression, SyntaxKind.MinusToken),
                "MULTIPLY" => (SyntaxKind.MultiplyExpression, SyntaxKind.AsteriskToken),
                "DIVIDE" => (SyntaxKind.DivideExpression, SyntaxKind.SlashToken),
                "REMAINDER" => (SyntaxKind.ModuloExpression, SyntaxKind.PercentToken),
                
                _ => throw new ArgumentOutOfRangeException()
            };
        
            return SyntaxFactory.BinaryExpression(syntaxKind, (ExpressionSyntax) left, SyntaxFactory.Token(syntaxToken), (ExpressionSyntax) right);
        }
    
        public static SyntaxNode Cast(SyntaxGenerator syntaxGenerator, CastExpression node)
        {
            var expression = Expression(syntaxGenerator, node.Expression);
            var type = TypeGenerators.Type(syntaxGenerator, node.Type);
            return syntaxGenerator.CastExpression(type, expression);
        }
    
        public static SyntaxNode FieldAccess(SyntaxGenerator syntaxGenerator, FieldAccessExpression node)
        {
            var expression = Expression(syntaxGenerator, node.Scope);
            var memberName = BodyGenerators.Name(syntaxGenerator, node.Name);
            return syntaxGenerator.MemberAccessExpression(expression, memberName);
        }
    
        public static SyntaxNode Literal(SyntaxGenerator syntaxGenerator, LiteralExpression node)
        {
            if (node.Kind is "com.github.javaparser.ast.expr.BooleanLiteralExpr")
            {
                var value = bool.Parse(node.Value);
                return value ? syntaxGenerator.TrueLiteralExpression() : syntaxGenerator.FalseLiteralExpression();
            }

            if (node.Kind is "com.github.javaparser.ast.expr.NullLiteralExpr")
            {
                return syntaxGenerator.NullLiteralExpression();
            }
            
            var literalKind = node.Kind switch
            {
                "com.github.javaparser.ast.expr.IntegerLiteralExpr" => SyntaxKind.NumericLiteralExpression,
                "com.github.javaparser.ast.expr.CharLiteralExpr" => SyntaxKind.CharacterLiteralExpression,
                "com.github.javaparser.ast.expr.StringLiteralExpr" => SyntaxKind.StringLiteralExpression,
                "com.github.javaparser.ast.expr.DoubleLiteralExpr" => SyntaxKind.NumericLiteralExpression,
                "com.github.javaparser.ast.expr.LongLiteralExpr" => SyntaxKind.NumericLiteralExpression,
                _ => throw new ArgumentOutOfRangeException()
            };

            var literalToken = new SyntaxToken();
            try
            {
                literalToken = node.Kind switch
                {
                    "com.github.javaparser.ast.expr.IntegerLiteralExpr" => SyntaxFactory.Literal(int.Parse(node.Value)),
                    "com.github.javaparser.ast.expr.CharLiteralExpr" => SyntaxFactory.Literal(node.Value[0]),
                    "com.github.javaparser.ast.expr.StringLiteralExpr" => SyntaxFactory.Literal(node.Value),
                    "com.github.javaparser.ast.expr.DoubleLiteralExpr" => SyntaxFactory.Literal(
                        double.Parse(node.Value.TrimEnd('f', 'F'))),
                    "com.github.javaparser.ast.expr.LongLiteralExpr" => SyntaxFactory.Literal(long.Parse(node.Value.TrimEnd('l', 'L'))),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            catch (Exception ex)
            {
                literalToken = SyntaxFactory.Literal(node.Value);
            }
            
            return SyntaxFactory.LiteralExpression(literalKind, literalToken);
        }
    
        public static SyntaxNode MethodCall(SyntaxGenerator syntaxGenerator, MethodCallExpression node)
        {
            var arguments = node?.Arguments?.Select(expression => Expression(syntaxGenerator, expression));
            
            if (node.Scope == null)
            {
                var name = BodyGenerators.Name(syntaxGenerator, node.Name);

                return syntaxGenerator.InvocationExpression(name, arguments);
            }
            
            //NOTE(MICHAEL): This is pretty hacky, but it gets around the differences in syntax trees here.
            var scope = Expression(syntaxGenerator, node.Scope);
            node.Name.Qualifier = new Qualifier {Identifier = CollapseScope((scope as ExpressionSyntax)!)};
            node.Name.Identifier = node.Name.Identifier.Replace("@", "");
            node.Name.Qualifier.Identifier = node.Name.Qualifier.Identifier.Replace("@", "");
            var scopedName = BodyGenerators.Name(syntaxGenerator, node.Name);
            
            return syntaxGenerator.InvocationExpression(scopedName, arguments);
        }
    
        public static SyntaxNode Name(SyntaxGenerator syntaxGenerator, NameExpression node)
        {
            return BodyGenerators.Name(syntaxGenerator, node.Name);
        }
    
        public static SyntaxNode This(SyntaxGenerator syntaxGenerator, ThisExpression node)
        {
            return syntaxGenerator.ThisExpression();
        }
    
        public static SyntaxNode Unary(SyntaxGenerator syntaxGenerator, UnaryExpression node)
        {
            var expression = (ExpressionSyntax) Expression(syntaxGenerator, node.Expression);

            var (syntaxKind, syntaxToken) = node.Operator switch
            {
                "PLUS" => (SyntaxKind.UnaryPlusExpression, SyntaxKind.PlusToken),
                "MINUS" => (SyntaxKind.UnaryMinusExpression, SyntaxKind.MinusToken),
                "BITWISE_COMPLEMENT" => (SyntaxKind.BitwiseNotExpression, SyntaxKind.TildeToken),
                "PREFIX_INCREMENT" => (SyntaxKind.PreIncrementExpression, SyntaxKind.PlusPlusToken),
                "PREFIX_DECREMENT" => (SyntaxKind.PreDecrementExpression, SyntaxKind.MinusMinusToken),
                "POSTFIX_INCREMENT" => (SyntaxKind.PostIncrementExpression, SyntaxKind.PlusPlusToken),
                "POSTFIX_DECREMENT" => (SyntaxKind.PostDecrementExpression, SyntaxKind.MinusMinusToken),
                "LOGICAL_COMPLEMENT" => (SyntaxKind.LogicalNotExpression, SyntaxKind.ExclamationToken),
                _ => throw new ArgumentOutOfRangeException()
            };

            var operandToken = SyntaxFactory.Token(syntaxToken);
            return node.Operator switch
            {
                "PLUS" => SyntaxFactory.PrefixUnaryExpression(syntaxKind, operandToken, expression),
                "MINUS" => SyntaxFactory.PrefixUnaryExpression(syntaxKind, operandToken, expression),
                "BITWISE_COMPLEMENT" => SyntaxFactory.PrefixUnaryExpression(syntaxKind, operandToken, expression),
                "PREFIX_INCREMENT" => SyntaxFactory.PrefixUnaryExpression(syntaxKind, operandToken, expression),
                "PREFIX_DECREMENT" => SyntaxFactory.PrefixUnaryExpression(syntaxKind, operandToken, expression),
                "POSTFIX_INCREMENT" => SyntaxFactory.PostfixUnaryExpression(syntaxKind, expression, operandToken),
                "POSTFIX_DECREMENT" => SyntaxFactory.PostfixUnaryExpression(syntaxKind, expression, operandToken),
                "LOGICAL_COMPLEMENT" => SyntaxFactory.PrefixUnaryExpression(syntaxKind, operandToken, expression),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    
        public static SyntaxNode VariableDeclaration(SyntaxGenerator syntaxGenerator, VariableDeclarationExpression node)
        {
            // Modifers on these declarations dont exist in C#, so we ignore them.

            var variable = node?.Variables?.First();
            var type = TypeGenerators.Type(syntaxGenerator, variable.JavaType);
            var identifier = SyntaxFactory.Identifier(variable.SimpleName.Identifier);
            var designation = SyntaxFactory.SingleVariableDesignation(identifier);
            var declarationExpression = SyntaxFactory.DeclarationExpression(type as TypeSyntax, designation);

            if (variable.Initializer == null)
            {
                return declarationExpression;
            }
            var initializer = Expression(syntaxGenerator, variable.Initializer);

            return SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, declarationExpression, initializer as ExpressionSyntax);
        }

        public static SyntaxNode Enclosed(SyntaxGenerator syntaxGenerator, EnclosedExpression node)
        {
            var inner = Expression(syntaxGenerator, node.Inner) as ExpressionSyntax;
            return SyntaxFactory.ParenthesizedExpression(inner);
        }

        public static SyntaxNode ArrayCreation(SyntaxGenerator syntaxGenerator, ArrayCreationExpression node)
        {
            // TODO (Michael): Add support for initialized values
            var type = TypeGenerators.Type(syntaxGenerator, node.Type) as TypeSyntax;
            var levels = node.Levels?
                .Select(level => Expression(syntaxGenerator, level.Dimension) as ExpressionSyntax);
            
            var rankSpecifier = SyntaxFactory.ArrayRankSpecifier(SyntaxFactory.SeparatedList<ExpressionSyntax>(levels));
            var arrayType = SyntaxFactory.ArrayType(type, SyntaxFactory.List(new[] {rankSpecifier}));
            return SyntaxFactory.ArrayCreationExpression(arrayType);
        }

        public static SyntaxNode ArrayAccess(SyntaxGenerator syntaxGenerator, ArrayAccessExpression node)
        {
            var name = node.Name;
            var indexExpressions = new List<SyntaxNode>();
            while(name is ArrayAccessExpression arrayAccessExpression)
            {
                indexExpressions.Add(Expression(syntaxGenerator, arrayAccessExpression.Index));
                name = arrayAccessExpression.Name;
            }

            indexExpressions.Reverse();
            var expression = Expression(syntaxGenerator, name);
            indexExpressions.Add(Expression(syntaxGenerator, node.Index));
            
            return syntaxGenerator.ElementAccessExpression(expression, indexExpressions);
        }

        public static SyntaxNode ArrayInitializer(SyntaxGenerator syntaxGenerator, ArrayInitializerExpression node)
        {
            var expressions = node?.Values.Select(value => Expression(syntaxGenerator, value));
            return SyntaxFactory.InitializerExpression(SyntaxKind.ArrayInitializerExpression,
                SyntaxFactory.SeparatedList(expressions));
        }

        public static SyntaxNode ObjectCreation(SyntaxGenerator syntaxGenerator, ObjectCreationExpression node)
        {
            var type = TypeGenerators.Type(syntaxGenerator, node.Type);
            var arguments = node?.Arguments?.Select(argument => Expression(syntaxGenerator, argument));
            return syntaxGenerator.ObjectCreationExpression(type, arguments);
        }

        public static SyntaxNode Super(SyntaxGenerator syntaxGenerator, SuperExpression node)
        {
            return syntaxGenerator.BaseExpression();
        }
        
        #region Helpers
        private static String CollapseScope(ExpressionSyntax scope)
        {
            return scope switch
            {
                MemberAccessExpressionSyntax memberAccess => $"{CollapseScope(memberAccess.Expression)}.{memberAccess.Name}",
                ThisExpressionSyntax thisExpression => "this",
                BaseExpressionSyntax baseExpressionSyntax => "base",
                IdentifierNameSyntax identifierName => $"{identifierName.Identifier.ValueText}",
                InvocationExpressionSyntax invocationExpression => invocationExpression.ToFullString(),
                ObjectCreationExpressionSyntax objectCreationExpression => $"new {objectCreationExpression.Type.ToString()}{objectCreationExpression.ArgumentList}",
                ElementAccessExpressionSyntax elementAccessExpression => elementAccessExpression.ToFullString(),
                null => "",
                _ => throw new ArgumentOutOfRangeException(nameof(scope), scope, null)
            };
        }
    
        #endregion
    }
}