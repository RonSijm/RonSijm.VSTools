namespace RonSijm.VSTools.Core.Logging.Features.Dispatching;

public interface IEventDispatcher
{
    Task Send<T>(T message) where T : class;
}