using System.ComponentModel;
using DynamicData.Binding;

using ReactiveUI;
using RonSijm.VSTools.CLI.UI.Base;
using RonSijm.VSTools.CLI.UI.Helpers;
using Terminal.Gui;

namespace RonSijm.VSTools.CLI.UI.Selection;

[Lifetime.Singleton]
public class CoreFunctionSelectionView : FrameView, IViewFor<CoreFunctionSelectionViewModel>
{
    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (CoreFunctionSelectionViewModel)value;
    }

    public CoreFunctionSelectionViewModel ViewModel { get; set; }

    private TypedListView<RenderableOption> _list;

    public CoreFunctionSelectionView(CoreFunctionSelectionViewModel viewModel)
    {
        ViewModel = viewModel;
        CreateListView();
    }

    private void CreateListView()
    {
        _list = new TypedListView<RenderableOption>
        {
            Title = $"Select function to use:{Environment.NewLine}",
            TextAlignment = TextAlignment.Left,
            X = 0,
            Y = 0,
            Width = Dim.Percent(100),
            Height = Dim.Sized(9),
            AutoSize = true,
            LayoutStyle = LayoutStyle.Computed
        };
        
        _list.SetSource(ViewModel!.AvailableFunctionDescriptions);
        _list.RedrawFunction = DrawFunctionToUseText;

        _list.SelectedItemChanged += option => ViewModel.SelectedOption = option;

        ViewModel.WhenPropertyChanged(x => x.SelectedFunctionOptionsModel).Subscribe(NewOptionsSelected);

        Add(_list);
    }

    private void NewOptionsSelected(PropertyValue<CoreFunctionSelectionViewModel, object> propertyValue)
    {
        if (propertyValue?.Value == null)
        {
            return;
        }

        foreach (var viewModelViewsForOption in ViewModel.ViewsForOptions)
        {
            Remove(viewModelViewsForOption);
        }

        var type = propertyValue.Value.GetType();
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();

        View referenceForHeight = _list;

        foreach (var property in properties)
        {
            var description = property.GetCustomAttribute<DescriptionAttribute>();

            if (description != null)
            {
                var descriptionLabel = new Label(description.Description)
                {
                    X = 0,
                    Y = Pos.Bottom(referenceForHeight) + LayoutSizeProvider.LayoutMargin,
                    Width = Dim.Fill(),
                    Height = 1
                };

                ViewModel.ViewsForOptions.Add(descriptionLabel);
                Add(descriptionLabel);
                referenceForHeight = descriptionLabel;
            }

            var label = new Label(property.Name)
            {
                X = 0,
                Y = Pos.Bottom(referenceForHeight) + LayoutSizeProvider.LayoutMargin,
                Width = Dim.Fill(),
                Height = 1
            };

            ViewModel.ViewsForOptions.Add(label);
            Add(label);
            referenceForHeight = label;
        }

        var button = new Button("Go")
        {
            X = 0,
            Y = Pos.Bottom(referenceForHeight) + 1 + LayoutSizeProvider.LayoutMargin,
            Width = Dim.Fill(),
            Height = 1,
            
        };

        ViewModel.ViewsForOptions.Add(button);
        Add(button);
        referenceForHeight = button;

        this.SetNeedsDisplay();
    }

    private void DrawFunctionToUseText(TypedListView<RenderableOption> sender)
    {
        if (ViewModel == null || sender.SelectedItem == -1)
        {
            sender.Title = $"Select function to use:{Environment.NewLine}";
            return;
        }

        var typedValue = sender.SelectedModel;

        var text = $"Selected function to use: {typedValue.DisplayName} {Environment.NewLine} - ";
        var wrappedLines = TextFormatter.WordWrapText(typedValue.Description, sender.Bounds.Width - 2, true);

        foreach (var wrappedLine in wrappedLines)
        {
            text += wrappedLine + Environment.NewLine;
        }

        if (sender.Title != text)
        {
            sender.Title = text;
            sender.SetNeedsDisplay();
        }
    }
}