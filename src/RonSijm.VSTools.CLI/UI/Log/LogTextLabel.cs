using System.Linq.Expressions;
using System.Text;
using RonSijm.VSTools.CLI.UI.Base;

namespace RonSijm.VSTools.CLI.UI.Log;

public class LogTextLabel(LogTextViewModel logTextViewModel) : BaseIntBasedUpdatableView<LogTextViewModel>(logTextViewModel)
{
    protected override Expression<Func<LogTextViewModel, object>> PropertyAccessor => model => model.Messages;
    protected override Func<int> GetState => () => ViewModel == null ? -1 : ViewModel.Messages.Count;

    protected override string CreateText()
    {
        var bob = new StringBuilder();

        if (ViewModel != null)
        {
            foreach (var logMessage in ViewModel.Messages)
            {
                bob.AppendLine($"[{logMessage.Received.ToShortTimeString()}] {logMessage.FormattedMessage}");
            }
        }

        var result = bob.ToString();

        return result;
    }
}