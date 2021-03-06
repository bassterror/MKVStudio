using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class ApplyToAllCommand : ICommand
    {
        private readonly ObservableCollection<VideoFileVM> _videos;
        private readonly IExternalLibrariesService _exLIb;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public ApplyToAllCommand(ObservableCollection<VideoFileVM> videos, IExternalLibrariesService externalLibrariesService)
        {
            _videos = videos;
            _exLIb = externalLibrariesService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ApplyToAllV applyToAllView = new();
            applyToAllView.DataContext = new ApplyToAllVM(_videos, _exLIb, applyToAllView);
            applyToAllView.Owner = Application.Current.MainWindow;
            applyToAllView.ShowInTaskbar = false;
            applyToAllView.ShowDialog();
        }
    }
}
