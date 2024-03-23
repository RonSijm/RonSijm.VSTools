using System.Threading.Tasks;
using MassTransit;
using RonSijm.VSTools.CLI.UI.Log;

namespace RonSijm.VSTools.CLI.Transit.Consumers;

public class LogMessageToLogViewConsumer(LogTextViewModel logTextViewModel) : IConsumer<LogMessage>
{
    public Task Consume(ConsumeContext<LogMessage> context)
    {
        logTextViewModel.Add(context.Message);
        return Task.CompletedTask;
    }
}