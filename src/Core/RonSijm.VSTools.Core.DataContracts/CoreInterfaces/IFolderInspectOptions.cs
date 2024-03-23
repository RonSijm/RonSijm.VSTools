namespace RonSijm.VSTools.Core.DataContracts.CoreInterfaces;

public interface IFolderInspectOptions
{
    List<string> DirectoriesToInspect { get; set; }
    List<string> ProjectReferences { get; set; }
}