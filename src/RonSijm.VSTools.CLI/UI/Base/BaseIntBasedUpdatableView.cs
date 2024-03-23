using System.ComponentModel;

namespace RonSijm.VSTools.CLI.UI.Base;

public abstract class BaseIntBasedUpdatableView<T>(T model) : BaseStateBasedUpdatableView<T, int>(model) where T : class, INotifyPropertyChanged;