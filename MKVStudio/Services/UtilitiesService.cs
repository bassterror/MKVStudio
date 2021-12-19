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
            openFileDialog.ShowDialog();
            return openFileDialog;
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

        public string GetFolder()
        {
            using FolderBrowserDialog fbd = new();
            DialogResult result = fbd.ShowDialog();
            return fbd.SelectedPath;
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


        private readonly string[] _sizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public string ConvertBytes(long value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException(nameof(decimalPlaces)); }
            if (value < 0) { return "-" + ConvertBytes(-value, decimalPlaces); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                _sizeSuffixes[mag]);
        }
        #endregion
    }
}
