using Microsoft.Win32;
using System;

namespace MKVStudio.Services
{
    public class RegistryService : IRegistryService
    {
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
            return key == null || CheckExternalLibrariesRegistries();
        }

        public static void CreateMKVStudioRegistryKey()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\MKVStudio", true);
            key.Close();
        }

        private static bool CheckExternalLibrariesRegistries()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
            object ffmpeg = key.GetValue(Executables.FFmpeg.ToString());
            object mkvInfo = key.GetValue(Executables.MKVInfo.ToString());
            object mkvMerge = key.GetValue(Executables.MKVMerge.ToString());
            object mkvPropEdit = key.GetValue(Executables.MKVPropEdit.ToString());
            object mkvExtract = key.GetValue(Executables.MKVExtract.ToString());

            return ffmpeg == null || mkvInfo == null || mkvMerge == null || mkvPropEdit == null || mkvExtract == null;
        }

        public static void CreateExternalLibrariesRegistries()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);

            key.SetValue(Executables.FFmpeg.ToString(), GetFile("ffmpeg.exe|ffmpeg.exe"));
            key.SetValue(Executables.MKVInfo.ToString(), GetFile("mkvinfo.exe|mkvinfo.exe"));
            key.SetValue(Executables.MKVMerge.ToString(), GetFile("mkvmerge.exe|mkvmerge.exe"));
            key.SetValue(Executables.MKVPropEdit.ToString(), GetFile("mkvpropedit.exe|mkvpropedit.exe"));
            key.SetValue(Executables.MKVExtract.ToString(), GetFile("mkvextract.exe|mkvextract.exe"));

            key.Close();
        }

        private static string GetFile(string filter)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = filter;
            openFileDialog.ValidateNames = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;
            return openFileDialog.ShowDialog() == true
                ? openFileDialog.FileName
                : throw new ArgumentException("The file you selected is not correct!");
        }
    }
}
