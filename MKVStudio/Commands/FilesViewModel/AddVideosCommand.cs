using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddVideosCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly ObservableCollection<VideoFileVM> _videos;
        private readonly IExternalLibrariesService _exLib;

        public AddVideosCommand(ObservableCollection<VideoFileVM> videos, IExternalLibrariesService externalLibrariesService)
        {
            _videos = videos;
            _exLib = externalLibrariesService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            foreach (string filename in _exLib.Util.GetFileDialog("Video files (*.mkv, *.mp4)|*.mkv;*.mp4|All files (*.*)|*.*", true).FileNames)
            {
                VideoFileVM video = new(filename, _exLib);
                _videos.Add(video);
            }
        }
    }
}
