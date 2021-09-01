using MKVStudio.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddVideosCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly ObservableCollection<Video> _videos;

        public AddVideosCommand(ObservableCollection<Video> videos)
        {
            _videos = videos;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Video files (*.mkv, *.mp4)|*.mkv;*.mp4|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //TODO remember last directory
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    Video video = new(filename);
                    _videos.Add(video);
                }
            }
        }
    }
}
