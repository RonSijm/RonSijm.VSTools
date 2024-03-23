using Terminal.Gui;

namespace RonSijm.VSTools.CLI.UI.Base;

public class ListViewAccessor : ListView
{
    public new void AddCommand(Command command, Func<bool?> func)
    {
        base.AddCommand(command, func);
    }
}