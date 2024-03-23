using RonSijm.VSTools.Core.DataContracts.NamespaceModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.SyntaxModels;

namespace RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Fixers;

[DebuggerDisplay("[{GetType().Name}]-[{MatchType}]: {CurrentItemDisplayValue} => {ExpectedItemDisplayValue}")]
public abstract class BaseSyntaxFixer : IFixable, IMatchedNamespace, ILoggableItem
{
    // ReSharper disable once EmptyConstructor - Justification - Used for debugging to break when fix is created.
    private protected BaseSyntaxFixer()
    {
    }

    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }
    public string CurrentItemValue { get; set; }
    public NamespaceChangeMatchType MatchType { get; set; }
    public SyntaxInFileToFixModel Parent { get; set; }

    public abstract void Fix();
}