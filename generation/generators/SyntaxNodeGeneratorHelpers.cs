using System;
using System.Collections.Generic;
using System.Linq;
using Generation.Java.Nodes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace Generation.Generators
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
                    _ => DeclarationModifiers.None
                };
            }).FirstOrDefault(modifier => modifier != DeclarationModifiers.None, DeclarationModifiers.None);
        }
    }
}