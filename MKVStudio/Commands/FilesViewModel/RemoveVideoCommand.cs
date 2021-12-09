using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveVideoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly ObservableCollection<VideoFileVM> _videos;

        public RemoveVideoCommand(ObservableCollection<VideoFileVM> videos)
        {
            _videos = videos;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is VideoFileVM video)
            {
                _ = _videos.Remove(video);
            }
        }
    }
}
