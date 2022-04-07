using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Generation.Rewriters
{
    public class VirtualMethodRewriter : CSharpSyntaxRewriter
    {
        private SemanticModel _semanticModel;

        public VirtualMethodRewriter(SemanticModel semanticModel)
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
                if (_semanticModel.GetDeclaredSymbol(node).IsAbstract)
                {
                    var methodSymbol = _semanticModel.GetDeclaredSymbol(methodDeclarationSyntax);
                    if (!methodSymbol.IsOverride && !methodSymbol.IsAbstract)
                    {
                        members = members.RemoveAt(i);
                        members = members.Insert(i, 
                            methodDeclarationSyntax.AddModifiers(SyntaxFactory.Token(SyntaxKind.VirtualKeyword)));
                    }
                }
            }


            node = node.WithMembers(members);
            return base.VisitClassDeclaration(node);
        }
    }
}