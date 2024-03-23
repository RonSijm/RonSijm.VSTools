using ReactiveUI;
using Terminal.Gui;

namespace RonSijm.VSTools.CLI.UI.Log;

[Lifetime.Singleton]
public class LogMessageView : FrameView, IViewFor<LogMessageViewModel>
{
    public LogMessageView(LogTextLabel logTextLabel)
    {
        AddLogTextLabel(logTextLabel);
    }

    private void AddLogTextLabel(LogTextLabel logTextLabel)
    {
        Add(logTextLabel);
    }
    
    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (LogMessageViewModel)value;
    }

    public LogMessageViewModel ViewModel { get; set; }
}