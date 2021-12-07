using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class ApplyToAllCommand : ICommand
    {
        private readonly ObservableCollection<VideoFileViewModel> _videos;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public ApplyToAllCommand(ObservableCollection<VideoFileViewModel> videos)
        {
            _videos = videos;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ApplyToAllView applyToAllView = new();
            applyToAllView.Content = new ApplyToAllViewModel(_videos);
            applyToAllView.Show();
        }
    }
}
