using MKVStudio.Services;
using MKVStudio.ViewModels;
using MKVStudio.Views;
using System.Windows;

namespace MKVStudio.Commands;

public class PreferencesCommand : BaseCommand
{
    private readonly MainVM _main;
    private readonly IUtilitiesService _util;

    public PreferencesCommand(MainVM main, IUtilitiesService util)
    {
        _main = main;
        _util = util;
    }

    public override void Execute(object parameter)
    {
        PreferencesV preferences = new();
        preferences.DataContext = new PreferencesVM(_main, _util, preferences);
        preferences.Owner = Application.Current.MainWindow;
        preferences.Show();
    }
}
