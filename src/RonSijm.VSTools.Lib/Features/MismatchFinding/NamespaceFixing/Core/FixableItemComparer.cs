namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.Core;

public class FixableItemComparer : IComparer<IReturnable>
{
    public int Compare(IReturnable item1, IReturnable item2)
    {
        if (item1 is QualifiedNameSyntaxFixer item1QualifiedNameSyntaxFixer && item2 is QualifiedNameSyntaxFixer item2QualifiedNameSyntaxFixer)
        {
            var nodeHandledValue = item1QualifiedNameSyntaxFixer.NoteHandledType - item2QualifiedNameSyntaxFixer.NoteHandledType;

            if (nodeHandledValue == 0)
            {
                return item1QualifiedNameSyntaxFixer.MatchType - item2QualifiedNameSyntaxFixer.MatchType;
            }

            return nodeHandledValue;
        }

        if (item1 is IMatchedNamespace item1Namespace && item2 is IMatchedNamespace item2Namespace)
        {
            return item1Namespace.MatchType - item2Namespace.MatchType;
        }

        return 0;
    }
}