using System;
using System.Collections.Generic;
using System.Linq;
using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public class ClassOrInterfaceSyntaxNodeGenerator : ISyntaxNodeGenerator<ClassOrInterface>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, ClassOrInterface node)
        {
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);
            var members = GenerateMembers(syntaxGenerator, node.Members);

            // TODO(MICHAEL): Generate where we are currently passing nulls
            
            return node.IsInterface 
                ? syntaxGenerator.InterfaceDeclaration(node.SimpleName.Identifier, null, accessibility, null, members) 
                : syntaxGenerator.ClassDeclaration(node.SimpleName.Identifier, null, accessibility, declarationModifiers, null, null, members);
        }

        private IEnumerable<SyntaxNode>? GenerateMembers(SyntaxGenerator syntaxGenerator, IEnumerable<Member>? members)
        {
            return members?.Select<Member, SyntaxNode>(member =>
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