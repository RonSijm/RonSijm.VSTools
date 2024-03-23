using RonSijm.VSTools.Core.DataContracts.FileModels;

namespace RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.SyntaxModels;

public class SyntaxesToFixCollection : FixableCollectionBase<SyntaxInFileToFixModel>
{
    public override string ObjectType => "Syntaxes";
}