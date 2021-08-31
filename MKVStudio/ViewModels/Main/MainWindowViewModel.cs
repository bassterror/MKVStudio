using MKVStudio.State.MainNavigator;
using MKVStudio.ViewModels.Base;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace MKVStudio.ViewModels.Main
{
    public class MainWindowViewModel : BaseMainViewModel
    {
        public IMainNavigator Navigator { get; set; } = new MainNavigator();

        public VideosViewModel SelectedVideo { get; set; }
        public ICommand AddVideosCommand { get; set; }
        public ICommand AddVideosFromFolderCommand { get; set; }
        public ICommand RemoveVideoCommand { get; set; }
        public ICommand ClearVideosCommand { get; set; }

        public MainWindowViewModel()
        {
            AddVideosCommand = new RelayCommand(AddVideos);
            AddVideosFromFolderCommand = new RelayCommand(AddVideosFromFolder);
            RemoveVideoCommand = new RelayCommand(RemoveVideo);
            ClearVideosCommand = new RelayCommand(ClearVideos);
        }

        private void AddVideos()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Video files (*.mkv, *.mp4)|*.mkv;*.mp4|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //TODO remember last directory
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    VideosViewModel video = new(filename);
                    Videos.Add(video);
                }
            }
        }

        private void AddVideosFromFolder()
        {
            using (FolderBrowserDialog fbd = new())
            {
                DialogResult result = fbd.ShowDialog();

                if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    foreach (string filename in GetFiles(fbd.SelectedPath, "*.mkv|*.mp4"))
                    {
                        VideosViewModel video = new(filename);
                        Videos.Add(video);
                    }
                }
            }
        }

        private void RemoveVideo()
        {
            _ = Videos.Remove(SelectedVideo);
        }

        private void ClearVideos()
        {
            Videos.Clear();
        }



        #region Help
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
        #endregion
    }
}
