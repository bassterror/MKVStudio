using MKVStudio.Services;
using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddVideosFromFolderCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly ObservableCollection<VideoFileViewModel> _videos;
        private readonly IExternalLibrariesService _exLib;

        public AddVideosFromFolderCommand(ObservableCollection<VideoFileViewModel> videos, IExternalLibrariesService externalLibrariesService)
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
            foreach (string filename in _exLib.Util.GetFilesFromFolder("*.mkv|*.mp4"))
            {
                VideoFileViewModel video = new(filename, _exLib);
                _videos.Add(video);
            }
        }
    }
}
