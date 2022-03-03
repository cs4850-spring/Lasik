using System;
using System.Linq;
using Generation.Generators.Expr;
using Generation.Generators.Types;
using Generation.Java.Nodes.Members;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Body
{
    public class FieldDeclarationSyntaxNodeGenerator : ISyntaxNodeGenerator<FieldDeclaration>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, FieldDeclaration node)
        {
            var variableDeclaration = GenerateVariableDeclaration(syntaxGenerator, node);
            
            var fieldDeclaration = SyntaxFactory.FieldDeclaration(variableDeclaration);
            
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);
            fieldDeclaration = AddAccessibilityModifier(fieldDeclaration, accessibility);
            fieldDeclaration = AddDeclarationModifiers(fieldDeclaration, declarationModifiers);
            return fieldDeclaration;

        }

        private static VariableDeclarationSyntax GenerateVariableDeclaration(SyntaxGenerator syntaxGenerator, FieldDeclaration node) 
        {
            var firstVariable = node.Variables.First();
            var type = SyntaxFactory.ParseTypeName(firstVariable.JavaType.Identifier());
            // NOTE (MICHAEL): We cant use the variable syntax node generator here as the field declaration 
            // requires declarators specifically.
            var variableDeclarators = node?.Variables?.Select(variable =>
            {
                var declarator = SyntaxFactory.VariableDeclarator(variable.SimpleName.Identifier);
                if (variable.Initializer == null) return declarator;
                
                var initializer = new ExpressionSyntaxNodeGenerator().Generate(syntaxGenerator, variable.Initializer);
                return declarator.WithInitializer(SyntaxFactory.EqualsValueClause((initializer as ExpressionSyntax)!));

            });
            
            return SyntaxFactory.VariableDeclaration(type, SyntaxFactory.SeparatedList(variableDeclarators!));
        }

        private static FieldDeclarationSyntax AddAccessibilityModifier(FieldDeclarationSyntax fieldDeclarationSyntax, Accessibility accessibility)
        {
            var accessibilityToken = accessibility switch
            {
                Accessibility.NotApplicable => SyntaxKind.None,
                Accessibility.Private => SyntaxKind.PrivateKeyword,
                Accessibility.ProtectedAndInternal => SyntaxKind.ProtectedKeyword,
                Accessibility.Protected => SyntaxKind.ProtectedKeyword,
                Accessibility.Internal => SyntaxKind.InternalKeyword,
                Accessibility.ProtectedOrInternal => SyntaxKind.ProtectedKeyword,
                Accessibility.Public => SyntaxKind.PublicKeyword,
                _ => throw new ArgumentOutOfRangeException(nameof(accessibility), accessibility, null)
            };

            return fieldDeclarationSyntax.AddModifiers(SyntaxFactory.Token(accessibilityToken));
        }
        
        private static FieldDeclarationSyntax AddDeclarationModifiers(FieldDeclarationSyntax fieldDeclarationSyntax, DeclarationModifiers declarationModifiers)
        {
            var modifierToken = SyntaxKind.None;
            if (declarationModifiers == DeclarationModifiers.Const)
            {
                modifierToken = SyntaxKind.ConstKeyword;
            } else if (declarationModifiers == DeclarationModifiers.Static)
            {
                modifierToken = SyntaxKind.StaticKeyword;
            } else if (declarationModifiers == DeclarationModifiers.ReadOnly)
            {
                modifierToken = SyntaxKind.ReadOnlyKeyword;
            }
            else
            {
                return fieldDeclarationSyntax;
            }

            return fieldDeclarationSyntax.AddModifiers(SyntaxFactory.Token(modifierToken));
        }

    }
}