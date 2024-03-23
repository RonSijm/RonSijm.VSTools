//using RonSijm.VSTools.CLI.Options.Services;
//using Spectre.Console;
//using Spectre.Console.Rendering;
//using System.Threading.Tasks;
//using RonSijm.VSTools.Core.Logging.Features.Dispatching;
//using Spectre.Console.Extensions;
//using Spectre.Console.Live.Progress;
//using Spectre.Console.Live.Progress.Columns;
//using Spectre.Console.Prompts;
//using Spectre.Console.Prompts.List;
//using Spectre.Console.Widgets;
//using Spectre.Console.Widgets.Layout;

//namespace RonSijm.VSTools.CLI.UI;

//[Lifetime.Singleton]
//public class LayoutService(ConsoleUpdateService consoleUpdateService, LogLayoutService logLayoutService, ILoggerProxy<LayoutService> logger)
//{

//    public async Task StartAsync()
//    {
//        var layout = new Layout("Root")
//            .SplitRows(new Layout("Header").Ratio(1), new Layout("Layout").Ratio(100)
//                .SplitColumns(
//                    new Layout("Left")
//                        .SplitRows(
//                            new Layout("Top"),
//                            new Layout("Bottom")),
//                    new Layout("Right")));

//        logLayoutService.Init(layout["Right"]);

//        var progressBarTask = new ProgressTask(1, "Testing", 100);
//        progressBarTask.StartTask();
//        progressBarTask.Description = "Just a test Bar";
//        progressBarTask.MaxValue = 1000;

//        MultiSelectionPrompt<string> multiSelectPrompt = null;
//        ListPromptState<string> state = null;
//        ListPrompt<string> prompt = null;
//        IRenderable select = null;
//        IListPromptStrategy<string> asInterface = null;

//        var layoutFunction = new Func<Layout>(() =>
//        {
//            AnsiConsole.Clear();

//            if (Console.WindowWidth < 70)
//            {
//                var errorLayout = new Layout("Root");

//                errorLayout["Root"].Update(new Panel(Align.Center(
//                    new Rows(
//                        new Markup("Console window is too small."),
//                        new Markup("Please enlarge your window.")), VerticalAlignment.Middle)).Expand());

//                AnsiConsole.Write(errorLayout);
//                return errorLayout;
//            }

//            layout["Header"].UpdateHeader();

//            logLayoutService.Update();

//            layout["Top"].Update(new Panel(Align.Center(new Markup("Hello [blue]Top![/]"), VerticalAlignment.Middle)).Expand());
            
//            progressBarTask.Increment(75);

//            var console = AnsiConsole.Console;
//            var renderContext = RenderOptions.Create(console, console.Profile.Capabilities);

//            var description = new TaskDescriptionColumn().Render(renderContext, progressBarTask, TimeSpan.FromSeconds(1));
//            var progressBar = new ProgressBarColumn().Render(renderContext, progressBarTask, TimeSpan.FromSeconds(1));
//            var transferBar = new SpinnerColumn().Render(renderContext, progressBarTask, TimeSpan.FromSeconds(1));

//            if (multiSelectPrompt == null || prompt == null || state == null || select == null)
//            {
//                multiSelectPrompt = new MultiSelectionPrompt<string>();
//                multiSelectPrompt.AddChoices("Hello", "Darkness", "My", "Old", "Friend");
//                state = new ListPromptState<string>(multiSelectPrompt.Tree._roots, 10, false);
//                prompt = new ListPrompt<string>(console, multiSelectPrompt);
//                asInterface = multiSelectPrompt;
//            }

//            select = prompt.BuildRenderable(state);

//            layout["Bottom"].Update(new Panel(new Rows(
//                Align.Left(select, VerticalAlignment.Middle),
//                Align.Left(description, VerticalAlignment.Middle),
//                Align.Left(progressBar, VerticalAlignment.Middle),
//                Align.Left(transferBar, VerticalAlignment.Middle))).Expand());

//            // Render the layout


//            try
//            {
//                AnsiConsole.Write(layout);
//            }
//            catch (Exception)
//            {
//                AnsiConsole.Write("Console window is too small.");
//                AnsiConsole.Write("Please enlarge your window.");
//            }

//            return layout;
//        });

//        layoutFunction();

//        consoleUpdateService.StartTask(layoutFunction);


//        var isInput = false;

//        var task = new Task(async () =>
//        {
//            while (true)
//            {
//                var key = Console.ReadKey(true);

//                asInterface.HandleInput(key, state);
//                state.Update(key.Key);

//                await logger.LogInformation(key.Key.ToString());
//                isInput = true;
//                await Task.Delay(100);
//            }
//        });

//        task.Start();
//    }
//}