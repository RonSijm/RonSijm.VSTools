using RonSijm.VSTools.Core.DataContracts.FileModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

namespace RonSijm.VSTools.Module.NamespaceFixer.FolderFixing.Models;

public class FoldersToFixCollection : FixableCollectionBase<IFixable>
{
    public override string ObjectType => "Project Folder Structure";
}