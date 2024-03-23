using Terminal.Gui;

namespace RonSijm.VSTools.CLI.UI.Base;

public abstract class BaseUpdatableLabel : Label
{
    protected BaseUpdatableLabel()
    {
        Update();
    }

    protected abstract Func<(string Text, bool NeedsUpdate)> GetUpdateFunction { get; }

    public override bool OnDrawFrames()
    {
        Update();
        return base.OnDrawFrames();
    }

    private void Update()
    {
        var result = GetUpdateFunction();

        if (result.NeedsUpdate)
        {
            Text = result.Text;
            Application.Wakeup();
        }
    }
}