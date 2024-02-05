namespace RonSijm.VSTools.CLI;

public class CLICore(ILogger<CLICore> logger, OptionsFromArgsParser optionsParser, OptionsLogger optionsLogger, VSToolFacade vsToolFacade, AfterRunOptionsHelper afterRunOptionsHelper)
{
    public void Start(string[] args)
    {
        var runOptions = new Action<ParsedCLIOptionsModel>(_ => { });

        do
        {
            logger.CleanConsole();

            // Reload the options, so that you can on-demand change the config file, and re-run the application
            var options = optionsParser.Load(args);

            if (options == null)
            {
                logger.LogWarning("Input parameters were invalid.");
                return;
            }

            runOptions(options);

            // If you're running silently, we don't have to loop, and close the application after finishing
            runOptions = options.Silent ? null : runOptions;

            optionsLogger.LogActions(options);

            var coreOptions = options.Convert();
            var result = vsToolFacade.Fix(coreOptions);

            if (options.UpdateConfig && options.OptionsFile != null)
            {
                coreOptions.SaveOptions(options.OptionsFile);
            }

            if (runOptions != null)
            {
                runOptions = afterRunOptionsHelper.PrintInputOptions(result, options);
                Console.WriteLine();
            }
        } while (runOptions != null);
    }
}