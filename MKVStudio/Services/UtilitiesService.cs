using Microsoft.Win32;
using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace MKVStudio.Services
{
    public class UtilitiesService : IUtilitiesService
    {
        #region Registry
        public enum Executables
        {
            FFmpeg,
            MKVInfo,
            MKVMerge,
            MKVPropEdit,
            MKVExtract
        }

        public string GetExecutable(Executables executable)
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
            string path = string.Empty;
            switch (executable)
            {
                case Executables.FFmpeg:
                    path = key.GetValue(Executables.FFmpeg.ToString()).ToString();
                    break;
                case Executables.MKVInfo:
                    path = key.GetValue(Executables.MKVInfo.ToString()).ToString();
                    break;
                case Executables.MKVMerge:
                    path = key.GetValue(Executables.MKVMerge.ToString()).ToString();
                    break;
                case Executables.MKVPropEdit:
                    path = key.GetValue(Executables.MKVPropEdit.ToString()).ToString();
                    break;
                case Executables.MKVExtract:
                    path = key.GetValue(Executables.MKVExtract.ToString()).ToString();
                    break;
            }
            return path;
        }

        public static bool CheckMKVStudioRegistryKey()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
            object ffmpeg = key.GetValue(Executables.FFmpeg.ToString());
            object mkvInfo = key.GetValue(Executables.MKVInfo.ToString());
            object mkvMerge = key.GetValue(Executables.MKVMerge.ToString());
            object mkvPropEdit = key.GetValue(Executables.MKVPropEdit.ToString());
            object mkvExtract = key.GetValue(Executables.MKVExtract.ToString());
            return key == null || ffmpeg == null || mkvInfo == null || mkvMerge == null || mkvPropEdit == null || mkvExtract == null;
        }

        public static void CreateMKVStudioRegistryKeys()
        {
            UtilitiesService util = new();
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\MKVStudio", true);
            key.SetValue(Executables.FFmpeg.ToString(), util.GetFileDialog("ffmpeg.exe|ffmpeg.exe").FileName);
            key.SetValue(Executables.MKVInfo.ToString(), util.GetFileDialog("mkvinfo.exe|mkvinfo.exe").FileName);
            key.SetValue(Executables.MKVMerge.ToString(), util.GetFileDialog("mkvmerge.exe|mkvmerge.exe").FileName);
            key.SetValue(Executables.MKVPropEdit.ToString(), util.GetFileDialog("mkvpropedit.exe|mkvpropedit.exe").FileName);
            key.SetValue(Executables.MKVExtract.ToString(), util.GetFileDialog("mkvextract.exe|mkvextract.exe").FileName);
            key.Close();
        }
        #endregion

        #region Misc
        public Microsoft.Win32.OpenFileDialog GetFileDialog(string filter, bool multiselect = false)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new();
            openFileDialog.Multiselect = multiselect;
            openFileDialog.Filter = filter;
            openFileDialog.ValidateNames = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;
            return openFileDialog.ShowDialog() == true
                ? openFileDialog
                : throw new ArgumentException("The file you selected is not correct!");
        }

        /// <summary>
        /// Gets files from selected folder
        /// </summary>
        /// <param name="complexFilter">use "|" to separate</param>
        /// <returns></returns>
        public string[] GetFilesFromFolder(string complexFilter)
        {
            using FolderBrowserDialog fbd = new();
            DialogResult result = fbd.ShowDialog();

            return !string.IsNullOrWhiteSpace(fbd.SelectedPath) ? GetFiles(fbd.SelectedPath, complexFilter) : Array.Empty<string>();
        }

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
