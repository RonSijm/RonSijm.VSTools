using System.ComponentModel;
using System.Linq.Expressions;
using DynamicData.Binding;
using ReactiveUI;

namespace RonSijm.VSTools.CLI.UI.Base;

public abstract class BaseUpdatableView<T> : BaseUpdatableLabel, IViewFor<T> where T : class, INotifyPropertyChanged
{
    protected abstract Expression<Func<T, object>> PropertyAccessor { get; }

    protected BaseUpdatableView(T model)
    {
        WireViewModel(model);
    }

    private void WireViewModel(T model)
    {
        ViewModel = model;
        ViewModel.WhenPropertyChanged(PropertyAccessor).Subscribe(_ =>
        {
            if (IsInitialized)
            {
                OnDrawFrames();
            }
        });
    }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (T)value;
    }

    public T ViewModel { get; set; }
}