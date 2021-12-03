using MKVStudio.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveVideoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly ObservableCollection<VideoFile> _videos;

        public RemoveVideoCommand(ObservableCollection<VideoFile> videos)
        {
            _videos = videos;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is VideoFile video)
            {
                _ = _videos.Remove(video);
            }
        }
    }
}
