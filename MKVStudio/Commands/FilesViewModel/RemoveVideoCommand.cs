using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveVideoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly ObservableCollection<VideoFileViewModel> _videos;

        public RemoveVideoCommand(ObservableCollection<VideoFileViewModel> videos)
        {
            _videos = videos;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is VideoFileViewModel video)
            {
                _ = _videos.Remove(video);
            }
        }
    }
}
