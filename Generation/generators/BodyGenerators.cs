using System;
using System.Collections.Generic;
using System.Linq;
using Generation.Java.Nodes;
using Generation.Java.Nodes.Members;
using Generation.Java.Nodes.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public static class BodyGenerators
    {
        public static SyntaxNode CompilationUnit(SyntaxGenerator syntaxGenerator, CompilationUnit node)
        {
            // Note(Michael): We want to always have System import available.
            node.Imports.Add(new Import {Name = new SimpleName {Identifier = "System"}});
            
            var imports = node.Imports.Select(import => Import(syntaxGenerator, import));
        
            var types = node.Types
                .Select(type => TypeGenerators.ClassOrInterface(syntaxGenerator, type));

            var declarations = imports.Concat(types);
            return syntaxGenerator.CompilationUnit(declarations);
        }
    
        public static SyntaxNode Import(SyntaxGenerator syntaxGenerator, Import node)
        {
            var name = Name(syntaxGenerator, node.Name);
            return syntaxGenerator.NamespaceImportDeclaration(name);
        }
    
        public static SyntaxNode Member(SyntaxGenerator syntaxGenerator, Member node)
        {
            return node switch
            {
                FieldDeclaration fieldDeclaration => FieldDeclaration(syntaxGenerator, fieldDeclaration),
                MethodDeclaration methodDeclaration => MethodDeclaration(syntaxGenerator, methodDeclaration),
                ClassOrInterfaceDeclaration classOrInterfaceDeclaration => ClassOrInterface(syntaxGenerator, classOrInterfaceDeclaration),
                ConstructorDeclaration constructorDeclaration => ConstructorDeclaration(syntaxGenerator, constructorDeclaration),
                _ => throw new System.NotImplementedException()
            };
        }
        
        public static SyntaxNode ClassOrInterface(SyntaxGenerator syntaxGenerator, ClassOrInterfaceDeclaration node)
        {
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);
            
            // Inner classes can be static in java but must not be static here
            declarationModifiers = declarationModifiers.WithIsStatic(false);
            var members =
                node?.Members?.Select(member => BodyGenerators.Member(syntaxGenerator, member));
            
            // Check if the node is a declaration. If So, we just want to return a TypeSyntax.
            // For instance: In `public Bar foo()` `Bar` is a not a declaration
            if (IsDeclaration(node))
            {
                return SyntaxFactory.ParseTypeName(node.SimpleName.Identifier);
            }
            
            var interfaceTypes = 
                node.ImplementedTypes.Select(type => TypeGenerators.Type(syntaxGenerator, type));
            var extendsType = 
                node.ExtendedTypes.Select(type => TypeGenerators.Type(syntaxGenerator, type)).FirstOrDefault(defaultValue: null);
            
            return node.IsInterface 
                ? syntaxGenerator.InterfaceDeclaration(node.SimpleName.Identifier, null, accessibility, interfaceTypes, members) 
                : syntaxGenerator.ClassDeclaration(node.SimpleName.Identifier, null, accessibility, declarationModifiers, extendsType, interfaceTypes, members);
        }
    
        
        public static SyntaxNode MethodDeclaration(SyntaxGenerator syntaxGenerator, MethodDeclaration node)
        {
            var parameters = node.Parameters?
                .Select(parameter => Parameter(syntaxGenerator, parameter));
            
            var returnType = SyntaxFactory.ParseTypeName(node.JavaType.Identifier());
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);


            var block = StatementGenerators.BlockStatement(syntaxGenerator, node.Body) as BlockSyntax;

            return syntaxGenerator.MethodDeclaration(node.SimpleName.Identifier, parameters, null, returnType, accessibility,
                declarationModifiers, block?.Statements);
        }
    
        public static SyntaxNode Name(SyntaxGenerator syntaxGenerator, SimpleName node)
        {
            var identifier = syntaxGenerator.IdentifierName(node.Identifier);
            if (node.Qualifier == null) return identifier;
            
            return syntaxGenerator.IdentifierName($"{node.Qualifier.Identifier}.{node.Identifier}");
        }
    
        public static SyntaxNode Parameter(SyntaxGenerator syntaxGenerator, Parameter node)
        {
            var type = TypeGenerators.Type(syntaxGenerator, node.JavaType);
                
            // Note(Michael): Parameters can never be in/out since these do not exist in java.
            // TODO(Michael): Implement initializers
            return syntaxGenerator.ParameterDeclaration(node.SimpleName.Identifier, type, null, RefKind.None);
        }
    
        public static SyntaxNode Qualifier(SyntaxGenerator syntaxGenerator, Qualifier node)
        {
            return syntaxGenerator.IdentifierName(node.Identifier);
        }

        public static SyntaxNode Variable(SyntaxGenerator syntaxGenerator, Variable node)
        {
            var type = TypeGenerators.Type(syntaxGenerator, node.JavaType);
            var initializer = ExpressionGenerators.Expression(syntaxGenerator, node.Initializer);
            return syntaxGenerator.LocalDeclarationStatement(type, node.SimpleName.Identifier, initializer);
        }

        public static SyntaxNode FieldDeclaration(SyntaxGenerator syntaxGenerator, FieldDeclaration node)
        {
            var variableDeclaration = GenerateVariableDeclaration(syntaxGenerator, node);
            
            var fieldDeclaration = SyntaxFactory.FieldDeclaration(variableDeclaration);
            
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);
            fieldDeclaration = AddAccessibilityModifier(fieldDeclaration, accessibility);
            fieldDeclaration = AddDeclarationModifiers(fieldDeclaration, declarationModifiers);
            return fieldDeclaration;

        }

        public static SyntaxNode ConstructorDeclaration(SyntaxGenerator syntaxGenerator, ConstructorDeclaration node)
        {
            var name = node.SimpleName.Identifier;
            var parameters = node?.Parameters?.Select(parameter => Parameter(syntaxGenerator, parameter));
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);

            var superCallStatement = node?.Body?.Statements?.Find(statement => statement is ExplicitConstructorInvocationStatement);

            IEnumerable<SyntaxNode> baseConstructorArguments = null;
            if (superCallStatement is ExplicitConstructorInvocationStatement superCall)
            {
                baseConstructorArguments = superCall.Arguments?.Select(argument => ExpressionGenerators.Expression(syntaxGenerator, argument));
            }
            
            var body = StatementGenerators.BlockStatement(syntaxGenerator, node.Body) as BlockSyntax;
            
            // NOTE (MICHAEL): The super statement in java IS ALWAYS to be the first statement in the body
            if (superCallStatement != null)
            {
                body = body.WithStatements(body.Statements.RemoveAt(0));
            }
            var constructorDeclaration = syntaxGenerator.ConstructorDeclaration(name, parameters, accessibility, declarationModifiers, baseConstructorArguments, body?.Statements) as ConstructorDeclarationSyntax;

            return constructorDeclaration;
        }

        #region Helpers
        private static bool IsDeclaration(ClassOrInterfaceDeclaration node)
        {
            return (node?.Modifiers == null && node?.Members == null) ||
                   (node?.Modifiers?.Count == 0 && node?.Members?.Count == 0);
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
                
                var initializer = ExpressionGenerators.Expression(syntaxGenerator, variable.Initializer);
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
        #endregion
    }
}