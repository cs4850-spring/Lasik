using System;
using System.Collections.Generic;
using System.Linq;
using Generation.Java.Nodes.Members;
using Generation.Java.Nodes.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators
{
    public class ClassOrInterfaceSyntaxNodeGenerator : ISyntaxNodeGenerator<ClassOrInterfaceJavaType>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, ClassOrInterfaceJavaType node)
        {
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);
            var members = GenerateMembers(syntaxGenerator, node.Members);

            // TODO(MICHAEL): Generate where we are currently passing nulls

            // Check if the node is a declaration. If So, we just want to return a TypeSyntax.
            // For instance: In `public Bar foo()` `Bar` is a not a declaration
            if ((node.Modifiers == null && node.Members == null) ||
                (node?.Modifiers?.Count == 0 && node?.Members?.Count == 0))
            {
                return SyntaxFactory.ParseTypeName(node.SimpleName.Identifier);
            }
            
            return node.IsInterface 
                ? syntaxGenerator.InterfaceDeclaration(node.SimpleName.Identifier, null, accessibility, null, members) 
                : syntaxGenerator.ClassDeclaration(node.SimpleName.Identifier, null, accessibility, declarationModifiers, null, null, members);
        }

        private IEnumerable<SyntaxNode>? GenerateMembers(SyntaxGenerator syntaxGenerator, IEnumerable<Member>? members)
        {
            return members?.Select(member =>
            {
                return member switch
                {
                    MemberVariable memberVariable => new MemberVariableSyntaxNodeGenerator().Generate(syntaxGenerator,
                        memberVariable),
                    MemberMethod memberMethod => new MemberMethodSyntaxNodeGenerator().Generate(syntaxGenerator,
                        memberMethod),
                    _ => throw new ArgumentOutOfRangeException(nameof(member), member, null)
                };
            });
        }
    }
}