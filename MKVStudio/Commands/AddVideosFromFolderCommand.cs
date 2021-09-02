using MKVStudio.Models;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddVideosFromFolderCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly ObservableCollection<Video> _videos;

        public AddVideosFromFolderCommand(ObservableCollection<Video> videos)
        {
            _videos = videos;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            using (FolderBrowserDialog fbd = new())
            {
                DialogResult result = fbd.ShowDialog();

                if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    foreach (string filename in GetFiles(fbd.SelectedPath, "*.mkv|*.mp4"))
                    {
                        Video video = new(filename);
                        _videos.Add(video);
                    }
                }
            }
        }

        /// <summary>
        /// Returns files with specific multiple filters
        /// </summary>
        /// <param name="searchPath">Source directory</param>
        /// <param name="complexFilter">Sequence of filters divided by '|'</param>
        /// <returns>string[]</returns>
        private static string[] GetFiles(string searchPath, string complexFilter)
        {
            ArrayList newList = new();
            string[] filters = complexFilter.Split("|");
            foreach (string filter in filters)
            {
                newList.AddRange(Directory.GetFiles(searchPath, filter));
            }

            return (string[])newList.ToArray(typeof(string));
        }
    }
}
