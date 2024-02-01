namespace RonSijm.VSTools.CLI;

public class Program
{
    public static void Main(string[] args)
    {
        ProgramSettings.ProgramFileExtension.CreateFileExtensionAssociation("VSTools", "Tool to Automatically fix VS Paths", $"--{nameof(CLIOptionsModel.OptionsFile)} \"%1\"");

        var serviceProvider = ServiceProviderFactory.BuildServiceProvider();

        var program = serviceProvider.GetRequiredService<CLICore>();
        program.Start(args);
    }
}