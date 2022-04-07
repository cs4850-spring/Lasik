using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generation.Rewriters
{
    public class OverrideMethodRewriter : CSharpSyntaxRewriter
    {

        private SemanticModel _semanticModel;

        public OverrideMethodRewriter(SemanticModel semanticModel)
        {
            _semanticModel = semanticModel;
        }

        public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var allBaseMembers = new List<ISymbol>();
            
            var baseType = _semanticModel.GetDeclaredSymbol(node).BaseType;
            while (baseType != null)
            {
                allBaseMembers.AddRange(baseType.GetMembers());
                baseType = baseType.BaseType;
            }
            
            
            var members = node.Members;
            for (var i = 0; i < node.Members.Count; i++)
            {
                if (!(node.Members[i] is MethodDeclarationSyntax methodDeclarationSyntax)) continue;
                foreach (var baseMember in allBaseMembers)
                {
                    if (methodDeclarationSyntax.Identifier.ToString() == baseMember.Name)
                    {
                        if (baseMember.IsAbstract || baseMember.IsVirtual)
                        {
                            members = members.RemoveAt(i);
                            members = members.Insert(i, 
                                methodDeclarationSyntax.AddModifiers(SyntaxFactory.Token(SyntaxKind.OverrideKeyword)));

                        }
                    }
                }
            }

 

            node = node.WithMembers(members);

            return base.VisitClassDeclaration(node);
            
        }
    }
}