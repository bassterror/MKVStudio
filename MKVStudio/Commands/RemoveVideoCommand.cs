using MKVStudio.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveVideoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly ObservableCollection<Video> _videos;

        public RemoveVideoCommand(ObservableCollection<Video> videos)
        {
            _videos = videos;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is Video video)
            {
                _ = _videos.Remove(video);
            }
        }
    }
}
