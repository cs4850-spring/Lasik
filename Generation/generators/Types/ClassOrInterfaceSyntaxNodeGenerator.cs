using System.Linq;
using Generation.Generators.Body;
using Generation.Java.Nodes.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Types
{
    public class ClassOrInterfaceSyntaxNodeGenerator : ISyntaxNodeGenerator<ClassOrInterfaceJavaType>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, ClassOrInterfaceJavaType node)
        {
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);
            var members =
                node?.Members?.Select(member => new MemberSyntaxNodeGenerator().Generate(syntaxGenerator, member));
            
            // Check if the node is a declaration. If So, we just want to return a TypeSyntax.
            // For instance: In `public Bar foo()` `Bar` is a not a declaration
            if (IsDeclaration(node))
            {
                return SyntaxFactory.ParseTypeName(node.SimpleName.Identifier);
            }
            
            var interfaceTypes =
                node.ImplementedTypes.Select(type => new TypeSyntaxNodeGenerator().Generate(syntaxGenerator, type));
            var extendsType =
                node.ExtendedTypes.Select(type => new TypeSyntaxNodeGenerator().Generate(syntaxGenerator, type))
                    .FirstOrDefault(defaultValue: null);
            
            return node.IsInterface 
                ? syntaxGenerator.InterfaceDeclaration(node.SimpleName.Identifier, null, accessibility, interfaceTypes, members) 
                : syntaxGenerator.ClassDeclaration(node.SimpleName.Identifier, null, accessibility, declarationModifiers, extendsType, interfaceTypes, members);
        }

        private static bool IsDeclaration(ClassOrInterfaceJavaType node)
        {
            return (node?.Modifiers == null && node?.Members == null) ||
                   (node?.Modifiers?.Count == 0 && node?.Members?.Count == 0);
        }
    }
}