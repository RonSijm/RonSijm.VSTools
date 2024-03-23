using Terminal.Gui;

namespace RonSijm.VSTools.CLI.UI.Header;

public class HeaderView : View
{
    public HeaderView()
    {
        AddHeaderLabel();
    }

    private void AddHeaderLabel()
    {
        Add(new HeaderLabel());
    }
}