using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands;

public class BrowseCommand : ICommand
{
    private readonly MultiplexVM _multiplex;
    private readonly IUtilitiesService _util;

    public event EventHandler CanExecuteChanged { add { } remove { } }

    public BrowseCommand(MultiplexVM multiplex, IUtilitiesService util)
    {
        _multiplex = multiplex;
        _util = util;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        // Set new output dir
        //_multiplex.OutputPath = _exLib.Util.GetFolder();
    }
}
