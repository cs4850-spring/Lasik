using System.Collections.Generic;
using System.Linq;
using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.generators
{
    public static class SyntaxNodeGeneratorHelpers
    {
        public static Accessibility AccessibilityFromModifiers(IEnumerable<Modifier>? modifiers)
        {
            if (modifiers == null) return Accessibility.NotApplicable;
            
            return modifiers.Select(modifier =>
            {
                return modifier.Keyword switch
                {
                    "PUBLIC" => Accessibility.Public,
                    "PRIVATE" => Accessibility.Private,
                    "PROTECTED" => Accessibility.Protected,
                    _ => Accessibility.NotApplicable
                };
            }).FirstOrDefault(access => access != Accessibility.NotApplicable, Accessibility.NotApplicable);
        }

        public static DeclarationModifiers DeclarationModifiersFromModifier(IEnumerable<Modifier>? modifiers)
        {
            if (modifiers == null) return DeclarationModifiers.None;

            return modifiers.Select(modifier =>
            {
                return modifier.Keyword switch
                {
                    "STATIC" => DeclarationModifiers.Static,
                    "FINAL" => DeclarationModifiers.ReadOnly,
                    "CONST" => DeclarationModifiers.Const,
                    "ABSTRACT" => DeclarationModifiers.Abstract,
                    _ => DeclarationModifiers.None
                };
            }).FirstOrDefault(modifier => modifier != DeclarationModifiers.None, DeclarationModifiers.None);
        }
        
        public static SyntaxNode AddTypeParamsAndConstraints(SyntaxGenerator syntaxGenerator, SyntaxNode syntax, List<TypeParameter> typeParameters)
        {
            var typeParameterStrings =
                typeParameters?.Select(typeParameter =>
                    BodyGenerators.TypeParameters(syntaxGenerator, typeParameter).ToString());

            var typeConstraints =
                typeParameters?.Select(typeParameters =>
                    typeParameters?.TypeBounds?.Select(type => TypeGenerators.Type(syntaxGenerator, type)));
            syntax = syntaxGenerator.WithTypeParameters(syntax, typeParameterStrings);

            for (var i = 0; i < typeConstraints.Count(); i++)
            {
                var constaints = typeConstraints.ElementAt(i);
                syntax = syntaxGenerator.WithTypeConstraint(syntax, typeParameterStrings.ElementAt(i),
                    SpecialTypeConstraintKind.None, constaints);
            }

            return syntax;
        }

        public static string Unbox(string identifier)
        {
            return identifier switch
            {
                "Boolean" => "bool",
                "Byte" => "byte",
                "Character" => "char",
                "Float" => "float",
                "Integer" => "int",
                "Long" => "long",
                "Short" => "short",
                "Double" => "double",
                _ => identifier
            };
        }
    }
}