using System.Collections.Generic;
using System.Linq;
using Generation.Generators.Types;
using Generation.Java.Nodes;
using Generation.Java.Nodes.Members;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators
{
    public class MemberMethodSyntaxNodeGenerator : ISyntaxNodeGenerator<MemberMethod>
    {
        public SyntaxNode Generate(SyntaxGenerator syntaxGenerator, MemberMethod node)
        {

            var parameters = GenerateParameters(syntaxGenerator, node.Parameters);
            
            var returnType = SyntaxFactory.ParseTypeName(node.JavaType.Identifier());
            var accessibility = SyntaxNodeGeneratorHelpers.AccessibilityFromModifiers(node.Modifiers);
            var declarationModifiers = SyntaxNodeGeneratorHelpers.DeclarationModifiersFromModifier(node.Modifiers);

            return syntaxGenerator.MethodDeclaration(node.SimpleName.Identifier, parameters, null, returnType, accessibility,
                declarationModifiers, null);
        }

        private IEnumerable<SyntaxNode>? GenerateParameters(SyntaxGenerator syntaxGenerator, List<Parameter> parameterNodes)
        {
            return parameterNodes?.Select<Parameter, SyntaxNode>(parameter =>
            {
                var type = new TypeSyntaxNodeGenerator().Generate(syntaxGenerator, parameter.JavaType);
                
                // Note(Michael): Parameters can never be in/out since these do not exist in java.
                return syntaxGenerator.ParameterDeclaration(parameter.SimpleName.Identifier, type, null, RefKind.None);
            });
        }
    }
}