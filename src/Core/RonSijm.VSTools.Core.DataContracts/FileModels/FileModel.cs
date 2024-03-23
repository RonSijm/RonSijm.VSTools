namespace RonSijm.VSTools.Core.DataContracts.FileModels;

[DebuggerDisplay("{FileName}")]
public class FileModel
{
    public string FileName { get; set; }
    public string LoadedFromDirectory { get; set; }
    public List<string> OtherNames { get; set; }
}