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
    private readonly IExternalLibrariesService _exLIb;

    public event EventHandler CanExecuteChanged { add { } remove { } }

    public BatchEditCommand(MultiplexerVM multiplexer, IExternalLibrariesService exLib)
    {
        _multiplexer = multiplexer;
        _exLIb = exLib;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        BatchEditV applyToAllView = new();
        applyToAllView.DataContext = new BatchEditVM(_multiplexer, _exLIb, applyToAllView);
        applyToAllView.Owner = Application.Current.MainWindow;
        applyToAllView.ShowInTaskbar = false;
        applyToAllView.ShowDialog();
    }
}
