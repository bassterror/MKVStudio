using Microsoft.Win32;
using System;

namespace MKVStudio.Services
{
    public class RegistryService : IRegistryService
    {
        public string GetFFmpeg()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
            object ffmpeg = key.GetValue("FFmpeg");
            return ffmpeg.ToString();
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
            object ffmpeg = key.GetValue("FFmpeg");
            object mkvInfo = key.GetValue("MKVInfo");
            object mkvMerge = key.GetValue("MKVMerge");
            object mkvPropEdit = key.GetValue("MKVPropEdit");
            object mkvExtract = key.GetValue("MKVExtract");

            return ffmpeg == null || mkvInfo == null || mkvMerge == null || mkvPropEdit == null || mkvExtract == null;
        }

        public static void CreateExternalLibrariesRegistries()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);

            key.SetValue("FFmpeg", GetFile("ffmpeg.exe|ffmpeg.exe"));
            key.SetValue("MKVInfo", GetFile("mkvinfo.exe|mkvinfo.exe"));
            key.SetValue("MKVMerge", GetFile("mkvmerge.exe|mkvmerge.exe"));
            key.SetValue("MKVPropEdit", GetFile("mkvpropedit.exe|mkvpropedit.exe"));
            key.SetValue("MKVExtract", GetFile("mkvextract.exe|mkvextract.exe"));

            key.Close();
        }

        private static string GetFile(string filter)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = filter;
            return openFileDialog.ShowDialog() == true
                ? openFileDialog.FileName
                : throw new ArgumentException("The file you selected is not correct!");
        }
    }
}
