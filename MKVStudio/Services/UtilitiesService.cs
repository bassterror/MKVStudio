using Microsoft.Win32;
using MKVStudio.Models;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static MKVStudio.Services.ExternalLibrariesService;
using F = System.Windows.Forms;

namespace MKVStudio.Services;

public class UtilitiesService : IUtilitiesService
{
    public IExternalLibrariesService ExLib { get; }
    public ObservableCollection<Language> AllLanguages { get; set; } = new();
    public ObservableCollection<Language> Languages { get; set; } = new();
    public SupportedFileTypesCollection SupportedFileTypesCollection { get; set; } = new();
    public MIMETypeCollection MIMETypes { get; set; } = new();

    public UtilitiesService(IExternalLibrariesService exLib)
    {
        ExLib = exLib;
        if (CheckMKVStudioRegistryKey())
        {
            CreateMKVStudioRegistryKeys();
        }
        GetLanguageList();
        GetSupportedFileTypes();
    }

    private async void GetLanguageList()
    {
        ProcessResult pr = await ExLib.Run(ProcessResultNames.MKVMergeLangList);
        string[] lines = pr.StdOutput.Split("\r\n");
        string[] pref = GetPreferedLanguages().Split("|");
        for (int i = 2; i < lines.Length - 1; i++)
        {
            Match match = Regex.Match(lines[i], @"^(.+)\|(.+)\|(.+)\|(.+)$");
            Language language = new(match);
            if (pref.Contains(language.ID))
            {
                Languages.Add(language);
            }
            else
            {
                AllLanguages.Add(language);
            }
        }
    }

    private async void GetSupportedFileTypes()
    {
        ProcessResult pr = await ExLib.Run(ProcessResultNames.MKVMergeSupportedFileTypes);
        string[] lines = pr.StdOutput.Split("\r\n");
        for (int i = 2; i < lines.Length - 1; i++)
        {
            Match match = Regex.Match(lines[i], @"^\s\s(.+)\s\[(.+)\]$");
            SupportedFileType fileType = new(match);
            SupportedFileTypesCollection.SupportedFileTypes.Add(fileType);
        }
    }


    #region Registry
    
    public bool CheckMKVStudioRegistryKey()
    {
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
        object ffmpeg = key.GetValue(Executables.FFmpeg.ToString());
        object mkvInfo = key.GetValue(Executables.MKVInfo.ToString());
        object mkvMerge = key.GetValue(Executables.MKVMerge.ToString());
        object mkvPropEdit = key.GetValue(Executables.MKVPropEdit.ToString());
        object mkvExtract = key.GetValue(Executables.MKVExtract.ToString());
        return key == null || ffmpeg == null || mkvInfo == null || mkvMerge == null || mkvPropEdit == null || mkvExtract == null;
    }

    public void CreateMKVStudioRegistryKeys()
    {
        RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\MKVStudio", true);
        key.SetValue(Executables.FFmpeg.ToString(), GetFileDialog("ffMPEG|ffmpeg.exe").FileName);
        key.SetValue(Executables.MKVInfo.ToString(), GetFileDialog("MKV Info|mkvinfo.exe").FileName);
        key.SetValue(Executables.MKVMerge.ToString(), GetFileDialog("MKV Merge|mkvmerge.exe").FileName);
        key.SetValue(Executables.MKVPropEdit.ToString(), GetFileDialog("MKV Prop Edit|mkvpropedit.exe").FileName);
        key.SetValue(Executables.MKVExtract.ToString(), GetFileDialog("MKV Extract|mkvextract.exe").FileName);
        key.Close();
    }

    public void SetPreferedLanguages(string languages)
    {
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
        key.SetValue("PreferedLanguages", languages);
        key.Close();
    }

    public string GetPreferedLanguages()
    {
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
        string value = key.GetValue("PreferedLanguages") != null ? key.GetValue("PreferedLanguages").ToString() : string.Empty;
        return value;
    }
    #endregion

    #region Misc
    public OpenFileDialog GetFileDialog(string filter, bool multiselect = false)
    {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Multiselect = multiselect;
        openFileDialog.Filter = filter;
        openFileDialog.ValidateNames = true;
        openFileDialog.CheckPathExists = true;
        openFileDialog.CheckFileExists = true;
        openFileDialog.ShowDialog();
        return openFileDialog;
    }

    public string[] GetFilesFromFolder(string complexFilter)
    {
        using F.FolderBrowserDialog fbd = new();
        F.DialogResult result = fbd.ShowDialog();

        return !string.IsNullOrWhiteSpace(fbd.SelectedPath) ? GetFiles(fbd.SelectedPath, complexFilter) : Array.Empty<string>();
    }

    public string GetFolder()
    {
        using F.FolderBrowserDialog fbd = new();
        F.DialogResult result = fbd.ShowDialog();

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
