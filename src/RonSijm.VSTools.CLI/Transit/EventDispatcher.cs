using System.Threading.Tasks;
using MassTransit;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;

namespace RonSijm.VSTools.CLI.Transit;

public class EventDispatcher(IBus messageBus) : IEventDispatcher
{
    public async Task Send<T>(T message) where T : class
    {
        await messageBus.Publish(message);
    }
}