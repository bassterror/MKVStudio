using MKVStudio.Services;
using MKVStudio.ViewModels;
using MKVStudio.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace MKVStudio.Commands;

public class BatchEditCommand : ICommand
{
    private readonly MultiplexerVM _multiplexer;
    private readonly IUtilitiesService _util;

    public event EventHandler CanExecuteChanged { add { } remove { } }

    public BatchEditCommand(MultiplexerVM multiplexer, IUtilitiesService util)
    {
        _multiplexer = multiplexer;
        _util = util;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        BatchEditV applyToAllView = new();
        applyToAllView.DataContext = new BatchEditVM(_multiplexer, _util, applyToAllView);
        applyToAllView.Owner = Application.Current.MainWindow;
        applyToAllView.ShowInTaskbar = false;
        applyToAllView.ShowDialog();
    }
}
