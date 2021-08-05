﻿using MKVStudio.Data;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;

namespace MKVStudio
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<Video> SelectedVideos { get; set; } = new();

        public ICommand AddVideosCommand { get; set; }
        public ICommand AddVideosFromFolderCommand { get; set; }

        public MainWindowViewModel()
        {
            AddVideosCommand = new RelayCommand(AddVideos);
            AddVideosFromFolderCommand = new RelayCommand(AddVideosFromFolder);
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
                    Video video = new(filename);
                    SelectedVideos.Add(video);
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
                        Video video = new(filename);
                        SelectedVideos.Add(video);
                    }
                }
            }
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