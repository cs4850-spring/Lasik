using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public class MemberMethodSyntaxNodeGenerator : ISyntaxNodeGenerator<MemberMethod>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, MemberMethod node)
        {

            var returnType = SyntaxFactory.ParseTypeName(node.Type?.SimpleName?.Identifier ?? "void");
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);
            return syntaxGenerator.MethodDeclaration(node.SimpleName.Identifier, null, null, returnType, accessibility,
                declarationModifiers, null);
        }
    }
}