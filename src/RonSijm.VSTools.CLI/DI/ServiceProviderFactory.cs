namespace RonSijm.VSTools.CLI.DI;

public static class ServiceProviderFactory
{
    public static IServiceCollection CreateServices(IServiceCollection services = null)
    {
        services ??= new ServiceCollection();

        AddTypesAndInterfaces(services, typeof(VSToolsHostedService));

        return services;
    }

    public static IServiceCollection RegisterVSToolsLib(this IServiceCollection services)
    {
        services.AddHostedService<VSToolsHostedService>();
        AddTypesAndInterfaces(services, typeof(VSToolsLibService));

        return services;
    }

    public static IServiceCollection WithOptions(this IServiceCollection services, ParsedCLIOptionsModel options)
    {
        services.AddSingleton(options);

        return services;
    }
    
    public static IServiceCollection WithLogging(this IServiceCollection services, string logSettingsFile = null)
    {
        var configuration = logSettingsFile == null ? 
            new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
                .AddJsonFile("VSTools.appsettings.json").Build() : 
            
            new ConfigurationBuilder()
                .AddJsonFile(logSettingsFile)
                .Build();

        services.AddLogging(loggingBuilder =>
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            loggingBuilder.AddSerilog(logger, dispose: true);

            Log.Logger = logger;
        });

        return services;
    }

    private static void AddTypesAndInterfaces(IServiceCollection services, Type targetType)
    {
        var types = targetType.Assembly.GetTypes().Where(x => !x.IsAbstract).ToList();

        foreach (var type in types)
        {
            services.AddTransient(type);

            var interfaces = type.GetInterfaces();

            foreach (var typeInterface in interfaces)
            {
                services.AddTransient(typeInterface, type);
            }
        }
    }
}