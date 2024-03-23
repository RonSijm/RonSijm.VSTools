using ReactiveUI;
using RonSijm.VSTools.CLI.Transit;

namespace RonSijm.VSTools.CLI.UI.Log;

[Lifetime.Singleton]
public class LogTextViewModel : ReactiveObject
{
    public void Add(LogMessage message)
    {
        _messages.Add(message);
        this.RaisePropertyChanged(nameof(Messages));
    }

    public IReadOnlyList<LogMessage> Messages => _messages;
    private readonly List<LogMessage> _messages = [];
}