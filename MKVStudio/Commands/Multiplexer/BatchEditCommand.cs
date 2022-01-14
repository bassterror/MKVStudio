using MKVStudio.Services;
using MKVStudio.ViewModels;
using MKVStudio.Views;
using System.Windows;

namespace MKVStudio.Commands;

public class BatchEditCommand : BaseCommand
{
    private readonly MultiplexerVM _multiplexer;
    private readonly IUtilitiesService _util;

    public BatchEditCommand(MultiplexerVM multiplexer, IUtilitiesService util)
    {
        _multiplexer = multiplexer;
        _util = util;
    }

    public override void Execute(object parameter)
    {
        BatchEditV applyToAllView = new();
        applyToAllView.DataContext = new BatchEditVM(_multiplexer, _util, applyToAllView);
        applyToAllView.Owner = Application.Current.MainWindow;
        applyToAllView.ShowInTaskbar = false;
        applyToAllView.ShowDialog();
    }
}
