//using System.Threading.Tasks;

//namespace RonSijm.VSTools.CLI.Options.Services;

//[Lifetime.Singleton]
//public class ConsoleUpdateService
//{
//    private bool _isListeningForSizeChanges;
//    private bool _isUpdating;
//    private bool _forceUpdate;
//    public int MinHeight = 10;
//    public int MinWidth = 10;

//    public void StartTask(Func<Layout> layoutUpdate)
//    {
//        // ReSharper disable once AsyncVoidLambda
//        var task = new Task(async () => await StartAsync(layoutUpdate));
//        task.Start();
//    }

//    public async Task StartAsync(Func<Layout> layoutUpdate)
//    {
//        layoutUpdate();

//        _isListeningForSizeChanges = true;
//        var height = Console.WindowHeight;
//        var width = Console.WindowWidth;

//        while (_isListeningForSizeChanges)
//        {
//            if (_isUpdating)
//            {
//                await Task.Delay(100);
//                continue;
//            }

//            var windowHeight = Console.WindowHeight;
//            var windowWidth = Console.WindowWidth;

//            if (height != windowHeight || width != windowWidth || _forceUpdate)
//            {
//                _isUpdating = true;
//                _forceUpdate = false;

//                if (MinHeight > Console.WindowHeight || MinWidth > Console.WindowWidth)
//                {
//                    Console.SetWindowSize(MinWidth, MinHeight);
//                    Console.SetBufferSize(MinWidth, MinHeight);
//                }
//                else
//                {
//                    layoutUpdate();

//                    height = Console.WindowHeight;
//                    width = Console.WindowWidth;
//                }

//                _isUpdating = false;
//            }

//            await Task.Delay(1000);
//        }

//        Console.WriteLine("Listner is off");
//    }

//    public void Update()
//    {
//        _forceUpdate = true;
//    }
//}