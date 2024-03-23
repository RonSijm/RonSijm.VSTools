using Terminal.Gui;
using Label = Terminal.Gui.Label;

namespace RonSijm.VSTools.CLI.UI.Base;

public class TypedListView<T> : View
{
    private ListViewAccessor _listView;
    private Label _title;

    private List<T> _source;

    public TypedListView()
    {
        SetupTitle();
        SetupListView();
    }

    public Action<T> SelectedItemChanged { get; set; }


    private void SetupListView()
    {
        _listView = new ListViewAccessor
        {
            X = 0,
            Y = Pos.Bottom(_title),
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            AllowsMarking = true,
            AllowsMultipleSelection = false,
        };

        _listView.RowRender += ListView_RowRender;

        if (_listView.AllowsMarking)
        {
            _listView.KeyBindings.Add(KeyCode.Enter, Command.ToggleChecked);
        }
        else
        {
            _listView.KeyBindings.Remove(KeyCode.Enter);
        }

        _listView.AddCommand(Command.ToggleChecked, () =>
        {
            _listView.MarkUnmarkRow();
            SelectedItemChanged?.Invoke(SelectedModel);

            return null;
        });

        Add(_listView);
    }

    private void SetupTitle()
    {
        _title = new Label
        {
            Text = $"Select function to use:{Environment.NewLine}",
            TextAlignment = TextAlignment.Left,
            X = 0,
            Y = 1, // for menu
            Width = Dim.Percent(100),
            AutoSize = true,
            LayoutStyle = LayoutStyle.Computed
        };

        Add(_title);
    }

    public new string Title
    {
        get => _title.Text;
        set => _title.Text = value;
    }

    public T SelectedModel
    {
        get
        {
            if (_listView.SelectedItem == -1)
            {
                return default;
            }

            return _source[_listView.SelectedItem];
        }
    }

    public Action<TypedListView<T>> RedrawFunction { get; set; }
    public int SelectedItem => _listView.SelectedItem;

    public void SetSource(List<T> source)
    {
        _source = source;
        _listView.SetSource(source);
    }

    public override bool OnDrawFrames()
    {
        RedrawFunction?.Invoke(this);
        return base.OnDrawFrames();
    }

    private void ListView_RowRender(object sender, ListViewRowEventArgs obj)
    {
        if (obj.Row == _listView.SelectedItem)
        {
            return;
        }

        if (_listView.AllowsMarking && _listView.Source.IsMarked(obj.Row))
        {
            obj.RowAttribute = new Terminal.Gui.Attribute(Color.BrightRed, Color.BrightYellow);
        }
    }
}