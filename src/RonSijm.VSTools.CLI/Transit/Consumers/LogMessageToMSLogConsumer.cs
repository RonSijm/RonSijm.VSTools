using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace RonSijm.VSTools.CLI.Transit.Consumers;

public class LogMessageToMSLogConsumer(ILogger<LogMessageToMSLogConsumer> logger) : IConsumer<LogMessage>
{
    public Task Consume(ConsumeContext<LogMessage> context)
    {
        logger.Log(context.Message.LogLevel, context.Message.Message, context.Message.Args);

        return Task.CompletedTask;
    }
}