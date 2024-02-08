namespace RonSijm.VSTools.Lib.Features.Core.Models;

[DebuggerDisplay("{FileName}")]
public class FileModel
{
    public string FileName { get; set; }
    public string LoadedFromDirectory { get; set; }
    public List<string> OtherNames { get; set; }
}