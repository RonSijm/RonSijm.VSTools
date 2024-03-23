using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;
using RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Helpers;

namespace RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Fixers;

[DebuggerDisplay("[{GetType().Name}]-[{MatchType}][{NoteHandledType}]: {CurrentItemDisplayValue} => {ExpectedItemDisplayValue}")]
public class QualifiedNameSyntaxFixer : BaseSyntaxFixer, IHaveObjectType
{
    public string ObjectType => "Qualified Name";

    public override void Fix()
    {
        var qualifiedNameSyntaxes = Parent.Root.DescendantNodes().OfType<QualifiedNameSyntax>().Where(x =>
        {
            var nodeHandleType = NodeHandleValidation.NodeHandled(x);
            if (NoteHandledType == nodeHandleType)
            {
                return x.ToString() == CurrentItemValue;
            }

            return false;
        }).ToList();

        if (qualifiedNameSyntaxes.Count > 1)
        {
            if (NoteHandledType != NodeHandledType.Unhandled)
            {
                var newQualifiedNameSyntaxes = new List<QualifiedNameSyntax> { qualifiedNameSyntaxes.FirstOrDefault() };

                qualifiedNameSyntaxes = newQualifiedNameSyntaxes;
            }
        }

        foreach (var qualifiedNameSyntax in qualifiedNameSyntaxes)
        {
            var newName = ExpectedItemValue.ToQualifiedName().WithTrailingTrivia(SyntaxFactory.Space);
            Parent.Root = Parent.Root.ReplaceNode(qualifiedNameSyntax, newName);
        }
    }

    public NodeHandledType NoteHandledType { get; set; }
}