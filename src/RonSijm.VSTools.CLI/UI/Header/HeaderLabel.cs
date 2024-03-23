using RonSijm.VSTools.CLI.UI.Base;
using RonSijm.VSTools.CLI.UI.Helpers;

namespace RonSijm.VSTools.CLI.UI.Header;

public class HeaderLabel : BaseUpdatableLabel
{
    private bool? _currentLayout;

    private const string HorizontalBanner =
            @"  ██████╗  ██████╗ ███╗   ██╗███████╗██╗     ██╗███╗   ███╗      ██╗   ██╗███████╗████████╗ ██████╗  ██████╗ ██╗     ███████╗ 
  ██╔══██╗██╔═══██╗████╗  ██║██╔════╝██║     ██║████╗ ████║      ██║   ██║██╔════╝╚══██╔══╝██╔═══██╗██╔═══██╗██║     ██╔════╝ 
  ██████╔╝██║   ██║██╔██╗ ██║███████╗██║     ██║██╔████╔██║      ██║   ██║███████╗   ██║   ██║   ██║██║   ██║██║     ███████╗ 
  ██╔══██╗██║   ██║██║╚██╗██║╚════██║██║██   ██║██║╚██╔╝██║      ╚██╗ ██╔╝╚════██║   ██║   ██║   ██║██║   ██║██║     ╚════██║ 
  ██║  ██║╚██████╔╝██║ ╚████║███████║██║╚█████╔╝██║ ╚═╝ ██║  ██╗  ╚████╔╝ ███████║   ██║   ╚██████╔╝╚██████╔╝███████╗███████║ 
  ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝╚═╝ ╚════╝ ╚═╝     ╚═╝  ╚═╝   ╚═══╝  ╚══════╝   ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝╚══════╝ "
        ;

    private const string VerticalBanner =
            @"  ██████╗  ██████╗ ███╗   ██╗███████╗██╗     ██╗███╗   ███╗       
  ██╔══██╗██╔═══██╗████╗  ██║██╔════╝██║     ██║████╗ ████║       
  ██████╔╝██║   ██║██╔██╗ ██║███████╗██║     ██║██╔████╔██║       
  ██╔══██╗██║   ██║██║╚██╗██║╚════██║██║██   ██║██║╚██╔╝██║       
  ██║  ██║╚██████╔╝██║ ╚████║███████║██║╚█████╔╝██║ ╚═╝ ██║       
  ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝╚═╝ ╚════╝ ╚═╝     ╚═╝    ██╗
                                                               ╚═╝
  ██╗   ██╗███████╗████████╗ ██████╗  ██████╗ ██╗     ███████╗
  ██║   ██║██╔════╝╚══██╔══╝██╔═══██╗██╔═══██╗██║     ██╔════╝
  ██║   ██║███████╗   ██║   ██║   ██║██║   ██║██║     ███████╗
  ╚██╗ ██╔╝╚════██║   ██║   ██║   ██║██║   ██║██║     ╚════██║
   ╚████╔╝ ███████║   ██║   ╚██████╔╝╚██████╔╝███████╗███████║
    ╚═══╝  ╚══════╝   ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝╚══════╝"
        ;

    public override void OnClicked()
    {
        ColorScheme = ColorSchemeProvider.DialogColorScheme;
        base.OnClicked();
    }

    protected override Func<(string Text, bool NeedsUpdate)> GetUpdateFunction => () =>
    {
        var horizonLayout = LayoutSizeProvider.HorizontalLayout;

        if (_currentLayout == horizonLayout)
        {
            return (null, false);
        }

        _currentLayout = horizonLayout;

        var result = horizonLayout ? HorizontalBanner : VerticalBanner;

        return (result, true);
    };
}