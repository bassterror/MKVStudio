using MKVStudio.Services;
using MKVStudio.ViewModels;

namespace MKVStudio.Commands;

public class BrowseCommand : BaseCommand
{
    private readonly MultiplexVM _multiplex;
    private readonly IUtilitiesService _util;

    public BrowseCommand(MultiplexVM multiplex, IUtilitiesService util)
    {
        _multiplex = multiplex;
        _util = util;
    }

    public override void Execute(object parameter)
    {
        // Set new output dir
        //_multiplex.OutputPath = _exLib.Util.GetFolder();
    }
}
