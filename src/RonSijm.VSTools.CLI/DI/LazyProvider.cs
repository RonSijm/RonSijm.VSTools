namespace RonSijm.VSTools.CLI.DI;

public class LazyProvider<T>(IServiceProvider provider) : Lazy<T>(provider.GetRequiredService<T>) where T : class;