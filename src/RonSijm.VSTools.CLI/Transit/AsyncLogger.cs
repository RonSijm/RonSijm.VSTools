using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;

namespace RonSijm.VSTools.CLI.Transit;

public class AsyncLogger<T>(IEventDispatcher eventDispatcher) : IAsyncLogger<T>
{
    public async Task Log(LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args)
    {
        await eventDispatcher.Send(new LogMessage(logLevel, eventId, exception, message, args));
    }
}