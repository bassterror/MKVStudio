using MKVStudio.Services;
using MKVStudio.ViewModels;
using MKVStudio.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace MKVStudio.Commands;

public class PreferencesCommand : ICommand
{
    private readonly MainVM _main;
    private readonly IUtilitiesService _util;

    public event EventHandler CanExecuteChanged { add { } remove { } }

    public PreferencesCommand(MainVM main, IUtilitiesService util)
    {
        _main = main;
        _util = util;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        PreferencesV preferences = new();
        preferences.DataContext = new PreferencesVM(_main, _util, preferences);
        preferences.Owner = Application.Current.MainWindow;
        preferences.Show();
    }
}
