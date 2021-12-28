using MKVStudio.Services;
using MKVStudio.ViewModels;
using MKVStudio.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class PreferencesCommand : ICommand
    {
        private readonly MainVM _main;
        private readonly IExternalLibrariesService _exLib;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public PreferencesCommand(MainVM main, IExternalLibrariesService exLib)
        {
            _main = main;
            _exLib = exLib;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PreferencesV preferences = new();
            preferences.DataContext = new PreferencesVM(_main, _exLib, preferences);
            preferences.Owner = Application.Current.MainWindow;
            preferences.Show();
        }
    }
}
