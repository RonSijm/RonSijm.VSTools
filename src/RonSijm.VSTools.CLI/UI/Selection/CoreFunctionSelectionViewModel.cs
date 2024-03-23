using DynamicData.Binding;
using ReactiveUI;

using RonSijm.VSTools.CLI.UI.Helpers;
using RonSijm.VSTools.Core.DataContracts;

using Terminal.Gui;

namespace RonSijm.VSTools.CLI.UI.Selection;

[Lifetime.Singleton]
public class CoreFunctionSelectionViewModel(IEnumerable<ICoreFunction> availableFunctions) : ReactiveObject
{
    private readonly IReadOnlyList<ICoreFunction> _availableFunctions = availableFunctions.ToList();

    public List<RenderableOption> AvailableFunctionDescriptions => _availableFunctions.Select(x => new RenderableOption { DisplayName = x.Function.ToString(), Description = x.FunctionDescription, Tag = x }).ToList();

    private RenderableOption _selectedOption;
    public RenderableOption SelectedOption
    {
        get => _selectedOption;
        set
        {
            _selectedOption = value;
            SelectedFunction = value.Tag as ICoreFunction;
        }
    }

    private ICoreFunction _selectedFunction;

    public ICoreFunction SelectedFunction
    {
        get => _selectedFunction;
        private set
        {
            _selectedFunction = value;

            if (value == null)
            {
                SelectedFunctionOptionsType = null;
                return;
            }

            SelectedFunctionOptionsType = value.GetGenericType(typeof(ICoreFunction<>));
        }
    }

    private Type _selectedFunctionOptionsType;


    public Type SelectedFunctionOptionsType
    {
        get => _selectedFunctionOptionsType;
        private set
        {
            _selectedFunctionOptionsType = value;

            if (value != null)
            {
                SelectedFunctionOptionsModel = Activator.CreateInstance(value);
            }
        }
    }

    private object _selectedFunctionOptionsModel;
    public object SelectedFunctionOptionsModel
    {
        get => _selectedFunctionOptionsModel;
        set => this.RaiseAndSetIfChanged(ref _selectedFunctionOptionsModel, value);
    }

    public List<View> ViewsForOptions { get; set; } = new();
}