using RonSijm.VSTools.Core.DataContracts.CoreModels;

namespace RonSijm.VSTools.Core.DataContracts.CoreInterfaces;

public interface IFolderFixOptions : IFolderInspectOptions
{
    FolderFixModeEnum FolderFixMode { get; set; }
}

public interface IHavePreview
{
    RunModeEnum RealRun { get; set; }
}