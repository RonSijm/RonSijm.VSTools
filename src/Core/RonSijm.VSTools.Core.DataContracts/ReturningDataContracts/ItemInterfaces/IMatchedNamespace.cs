using RonSijm.VSTools.Core.DataContracts.NamespaceModels;

namespace RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

public interface IMatchedNamespace
{
    NamespaceChangeMatchType MatchType { get; set; }
}