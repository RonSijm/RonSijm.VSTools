using DynamicData.Binding;

using ReactiveUI;
using RonSijm.VSTools.CLI.UI.Header;
using RonSijm.VSTools.CLI.UI.Helpers;
using RonSijm.VSTools.CLI.UI.Log;
using RonSijm.VSTools.CLI.UI.Selection;
using RonSijm.VSTools.Lib;
using Terminal.Gui;

namespace RonSijm.VSTools.CLI.UI.Main;

public class MainWindowView : Toplevel, IViewFor<MainWindowViewModel>
{
    private CoreFunctionSelectionView _coreFunctionSelectionView;

    public MainWindowView(MainWindowViewModel viewModel, CoreFunctionSelectionView coreFunctionSelectionView, LogMessageView logMessageView, VSToolsLibService vstoolsLibService, ParsedCLIOptionsModel options)
    {
        _coreFunctionSelectionView = coreFunctionSelectionView;
        ViewModel = viewModel;

        // MainWindowView inits the inner views, instead of having these properties inside the views themselves,
        // Because these are properties of where the views are IN THE MAIN WINDOW.
        // So for re-usability of components, the consumer (MainWindowView) decides these properties.
        var header = InitHeaderView();
        InitOptionsSelectionView(coreFunctionSelectionView, header);
        InitLogMessageView(logMessageView, header, coreFunctionSelectionView);

        Add(header, coreFunctionSelectionView, logMessageView);
    }

    public override void BeginInit()
    {
        base.BeginInit();
        Application.OnSizeChanging(new SizeChangedEventArgs(new Size(Console.WindowWidth, Console.WindowHeight)));
    }

    private static HeaderView InitHeaderView()
    {
        var header = new HeaderView
        {
            Title = "Header",
            X = Pos.Center(),
            Y = 0 + LayoutSizeProvider.LayoutMargin,
            Width = LayoutSizeProvider.HeaderWidthDim,
            Height = LayoutSizeProvider.HeaderSizeDim
        };

        return header;
    }

    private static void InitOptionsSelectionView(CoreFunctionSelectionView coreFunctionSelectionView, HeaderView header)
    {
        coreFunctionSelectionView.Title = "Options";
        coreFunctionSelectionView.X = 0 + LayoutSizeProvider.LayoutMargin;
        coreFunctionSelectionView.Y = Pos.Bottom(header) + LayoutSizeProvider.LayoutMargin;
        coreFunctionSelectionView.Width = Dim.Percent(50) - LayoutSizeProvider.LayoutMargin;
        coreFunctionSelectionView.Height = Dim.Fill() - LayoutSizeProvider.LayoutMargin;
    }

    private static void InitLogMessageView(View logMessageView, View topHeader, View leftSibling)
    {
        logMessageView.Title = "Logging";
        logMessageView.X = Pos.Right(leftSibling) + LayoutSizeProvider.LayoutMargin;
        logMessageView.Y = Pos.Bottom(topHeader) + LayoutSizeProvider.LayoutMargin;
        logMessageView.Width = Dim.Percent(50) - LayoutSizeProvider.LayoutMargin;
        logMessageView.Height = Dim.Fill() - LayoutSizeProvider.LayoutMargin;
    }

    public MainWindowViewModel ViewModel { get; set; }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (MainWindowViewModel)value;
    }
}