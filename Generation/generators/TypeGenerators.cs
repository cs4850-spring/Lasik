using System;
using System.Collections.Generic;
using System.Linq;
using Generation.Java.Nodes;
using Generation.Java.Nodes.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public static class TypeGenerators
    {
        public static SyntaxNode Type(SyntaxGenerator syntaxGenerator, JavaType node)
        {
            return node switch
            {
                ArrayJavaType arrayType => Array(syntaxGenerator, arrayType),
                ClassOrInterfaceJavaType classOrInterfaceType => ClassOrInterface(syntaxGenerator, classOrInterfaceType),
                PrimitiveJavaType primativeType => Primitive(syntaxGenerator, primativeType),
                VarJavaType varType => Var(syntaxGenerator, varType),
                VoidJavaType voidType => Void(syntaxGenerator, voidType),
                _ => throw new ArgumentOutOfRangeException(nameof(node))
            };
        }
    
        public static SyntaxNode Array(SyntaxGenerator syntaxGenerator, ArrayJavaType node)
        {
            var component = Type(syntaxGenerator, node.ComponentType);

            return syntaxGenerator.ArrayTypeExpression(component);
        }
    
        public static SyntaxNode ClassOrInterface(SyntaxGenerator syntaxGenerator, ClassOrInterfaceJavaType node)
        {
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);
            var members =
                node?.Members?.Select(member => BodyGenerators.Member(syntaxGenerator, member));
            var typeArguments =
                node?.TypeArguments?.Select(typeArgument => TypeGenerators.Type(syntaxGenerator, typeArgument));
            node.SimpleName.Identifier = SyntaxNodeGeneratorHelpers.Unbox(node.SimpleName.Identifier);

            // Check if the node is a declaration. If So, we just want to return a TypeSyntax.
            // For instance: In `public Bar foo()` `Bar` is a not a declaration
            if (IsDeclaration(node))
            {
                var typeSyntax = SyntaxFactory.ParseTypeName(node.SimpleName.Identifier);
                if (typeArguments?.Count() > 0)
                {
                    return syntaxGenerator.WithTypeArguments(typeSyntax, typeArguments);
                }
                else
                {
                    return typeSyntax;
                }
            }
            
            var interfaceTypes = 
                node.ImplementedTypes.Select(type => Type(syntaxGenerator, type));
            var extendsType = 
                node.ExtendedTypes.Select(type => Type(syntaxGenerator, type)).FirstOrDefault(defaultValue: null);
            
            var syntax = node.IsInterface 
                ? syntaxGenerator.InterfaceDeclaration(node.SimpleName.Identifier, null, accessibility, interfaceTypes, members) 
                : syntaxGenerator.ClassDeclaration(node.SimpleName.Identifier, null, accessibility, declarationModifiers, extendsType, interfaceTypes, members);
            
            return SyntaxNodeGeneratorHelpers.AddTypeParamsAndConstraints(syntaxGenerator, syntax, node?.TypeParameters);
        }



        public static SyntaxNode Primitive(SyntaxGenerator syntaxGenerator, PrimitiveJavaType node)
        {
            return SyntaxFactory.ParseTypeName(node.Identifier());
        }
    
        public static SyntaxNode Var(SyntaxGenerator syntaxGenerator, VarJavaType node)
        {
            return SyntaxFactory.ParseTypeName(node.Identifier());
        }
    
        public static SyntaxNode Void(SyntaxGenerator syntaxGenerator, VoidJavaType node)
        {
            return SyntaxFactory.ParseTypeName(node.Identifier());
        }
    
        private static bool IsDeclaration(ClassOrInterfaceJavaType node)
        {
            return (node?.Modifiers == null && node?.Members == null) ||
                   (node?.Modifiers?.Count == 0 && node?.Members?.Count == 0);
        }
    }
}