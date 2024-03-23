using System.ComponentModel;

namespace RonSijm.VSTools.CLI.UI.Base;

public abstract class BaseStateBasedUpdatableView<T, TStateType>(T model) : BaseUpdatableView<T>(model) where T : class, INotifyPropertyChanged
{
    private TStateType _previousState;
    protected abstract Func<TStateType> GetState { get; }
    protected abstract string CreateText();

    protected override Func<(string Text, bool NeedsUpdate)> GetUpdateFunction => () =>
    {
        if (ViewModel == null)
        {
            return (null, false);
        }

        var currentCount = GetState();

        if (currentCount.Equals(_previousState))
        {
            return (null, false);
        }

        _previousState = currentCount;

        var result = CreateText();

        return (result, true);
    };
}