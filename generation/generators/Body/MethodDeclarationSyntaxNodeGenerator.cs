using System.Collections.Generic;
using System.Linq;
using Generation.Generators.Types;
using Generation.Java.Nodes;
using Generation.Java.Nodes.Members;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators.Body
{
    public class MethodDeclarationSyntaxNodeGenerator : ISyntaxNodeGenerator<MethodDeclaration>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, MethodDeclaration node)
        {
            var parameters = node.Parameters?.Select(parameter =>
                new ParameterSyntaxNodeGenerator().Generate(syntaxGenerator, parameter));
            
            var returnType = SyntaxFactory.ParseTypeName(node.JavaType.Identifier());
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);

            // TODO(MICHAEL): Implement statements
            return syntaxGenerator.MethodDeclaration(node.SimpleName.Identifier, parameters, null, returnType, accessibility,
                declarationModifiers, null);
        }
    }
}