using Microsoft.Extensions.DependencyInjection;
using RonSijm.VSTools.CLI.DI;

namespace RonSijm.VSTools.Tests;

public abstract class BaseTestFor<T>
{
    protected BaseTestFor()
    {
        var serviceProvider = ServiceProviderFactory.BuildServiceProvider();
        SUT = serviceProvider.GetRequiredService<T>();
    }

    public T SUT { get; set; }
}