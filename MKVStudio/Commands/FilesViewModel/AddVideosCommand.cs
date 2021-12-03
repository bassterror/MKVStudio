using MKVStudio.Models;
using MKVStudio.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddVideosCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly ObservableCollection<VideoFile> _videos;
        private readonly IExternalLibrariesService _exLib;

        public AddVideosCommand(ObservableCollection<VideoFile> videos, IExternalLibrariesService externalLibrariesService)
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
                VideoFile video = new(filename, _exLib);
                _videos.Add(video);
            }
        }
    }
}
