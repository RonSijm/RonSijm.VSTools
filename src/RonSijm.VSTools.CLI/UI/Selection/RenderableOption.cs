namespace RonSijm.VSTools.CLI.UI.Selection;

public class RenderableOption
{
    public string DisplayName { get; set; }
    public string Description { get; set; }

    public object Tag { get; set; }

    public override string ToString()
    {
        return $"{DisplayName}";
    }
}