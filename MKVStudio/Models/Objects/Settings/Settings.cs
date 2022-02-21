using Microsoft.Win32;
using MKVStudio.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace MKVStudio.Models;

public class Settings
{
    private readonly IUtilitiesService _util;

    public ObservableCollection<Language> AllLanguages { get; set; } = new();
    public ObservableCollection<Language> PreferedLanguages { get; set; } = new();
    public SupportedFileTypesCollections SupportedFileTypes { get; set; } = new();
    public MIMETypeCollection MIMETypes { get; set; } = new();

    public Settings(IUtilitiesService util)
    {
        _util = util;
        if (CheckMKVStudioRegistryKey())
        {
            CreateMKVStudioRegistryKeys();
        }
        GetLanguageList();
        GetSupportedFileTypes();
    }

    #region Registry
    public bool CheckMKVStudioRegistryKey()
    {
        string ffmpegPath = null;
        string mkvInfoPath = null;
        string mkvMergePath = null;
        string mkvPropEditPath = null;
        string mkvExtractPath = null;

        using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
        if (key != null)
        {
            ffmpegPath = (string)key.GetValue(ExecutableNames.FFmpeg.ToString());
            if (ffmpegPath != null)
            {
                Executable ffmpeg = new(ExecutableNames.FFmpeg, ffmpegPath);
                _util.ExLib.Executables[ExecutableNames.FFmpeg] = ffmpeg;
            }
            mkvInfoPath = (string)key.GetValue(ExecutableNames.MKVInfo.ToString());
            if (mkvInfoPath != null)
            {
                Executable mkvInfo = new(ExecutableNames.MKVInfo, mkvInfoPath);
                _util.ExLib.Executables[ExecutableNames.MKVInfo] = mkvInfo;
            }
            mkvMergePath = (string)key.GetValue(ExecutableNames.MKVMerge.ToString());
            if (mkvMergePath != null)
            {
                Executable mkvMerge = new(ExecutableNames.MKVMerge, mkvMergePath);
                _util.ExLib.Executables[ExecutableNames.MKVMerge] = mkvMerge;
            }
            mkvPropEditPath = (string)key.GetValue(ExecutableNames.MKVPropEdit.ToString());
            if (mkvPropEditPath != null)
            {
                Executable mkvPropEdit = new(ExecutableNames.MKVPropEdit, mkvPropEditPath);
                _util.ExLib.Executables[ExecutableNames.MKVPropEdit] = mkvPropEdit;
            }
            mkvExtractPath = (string)key.GetValue(ExecutableNames.MKVExtract.ToString());
            if (mkvExtractPath != null)
            {
                Executable mkvExtract = new(ExecutableNames.MKVExtract, mkvExtractPath);
                _util.ExLib.Executables[ExecutableNames.MKVExtract] = mkvExtract;
            }
        }
        return key == null || ffmpegPath == null || mkvInfoPath == null || mkvMergePath == null || mkvPropEditPath == null || mkvExtractPath == null;
    }

    public void CreateMKVStudioRegistryKeys()
    {
        RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\MKVStudio", true);
        Executable ffmpeg = new(ExecutableNames.FFmpeg, _util.GetFileDialog("ffMPEG|ffmpeg.exe").FileName);
        Executable mkvInfo = new(ExecutableNames.MKVInfo, _util.GetFileDialog("MKV Info|mkvinfo.exe").FileName);
        Executable mkvMerge = new(ExecutableNames.MKVMerge, _util.GetFileDialog("MKV Merge|mkvmerge.exe").FileName);
        Executable mkvPropEdit = new(ExecutableNames.MKVPropEdit, _util.GetFileDialog("MKV Prop Edit|mkvpropedit.exe").FileName);
        Executable mkvExtract = new(ExecutableNames.MKVExtract, _util.GetFileDialog("MKV Extract|mkvextract.exe").FileName);
        _util.ExLib.Executables[ExecutableNames.FFmpeg] = ffmpeg;
        _util.ExLib.Executables[ExecutableNames.MKVInfo] = mkvInfo;
        _util.ExLib.Executables[ExecutableNames.MKVMerge] = mkvMerge;
        _util.ExLib.Executables[ExecutableNames.MKVPropEdit] = mkvPropEdit;
        _util.ExLib.Executables[ExecutableNames.MKVExtract] = mkvExtract;
        foreach (Executable executable in _util.ExLib.Executables.Values)
        {
            key.SetValue(executable.Name.ToString(), executable.Path);
        }
        key.Close();
    }
    #endregion

    #region Languages
    private async void GetLanguageList()
    {
        ProcessResult pr = await _util.ExLib.RunProcess(ProcessResultNames.MKVMergeLangList);
        string[] lines = pr.StdOutput.Split("\r\n");
        string[] pref = GetPreferedLanguages().Split("|");
        for (int i = 2; i < lines.Length - 1; i++)
        {
            Match match = Regex.Match(lines[i], @"^(.+)\|(.+)\|(.+)\|(.+)$");
            Language language = new(match);
            if (pref.Contains(language.ID))
            {
                PreferedLanguages.Add(language);
            }
            else
            {
                AllLanguages.Add(language);
            }
        }
    }

    private static string GetPreferedLanguages()
    {
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
        string value = key.GetValue("PreferedLanguages") != null ? key.GetValue("PreferedLanguages").ToString() : SetDefaultPreferedLanguages();
        return value;
    }

    private static string SetDefaultPreferedLanguages()
    {
        string value = "eng|und";
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
        key.SetValue("PreferedLanguages", value);
        return value;
    }

    public void SetPreferedLanguages(string languages)
    {
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\MKVStudio", true);
        key.SetValue("PreferedLanguages", languages);
        key.Close();
    }
    #endregion

    #region FileTypes
    private async void GetSupportedFileTypes()
    {
        Dictionary<string, string> uniqueExts = new();
        string exts = string.Empty;
        ProcessResult pr = await _util.ExLib.RunProcess(ProcessResultNames.MKVMergeSupportedFileTypes);
        string[] lines = pr.StdOutput.Split("\r\n");
        for (int i = 2; i < lines.Length - 1; i++)
        {
            Match match = Regex.Match(lines[i], @"^\s\s(.+)\s\[(.+)\]$");
            SupportedFileType fileType = new(match);
            SupportedFileTypes.SupportedFileTypes.Add(fileType);
            foreach (string ext in fileType.Extensions.Split(";"))
            {
                _ = uniqueExts.TryAdd(ext, ext);
            }
        }
        foreach (string extension in uniqueExts.Keys)
        {
            exts += $"{extension};";
        }
        SupportedFileType allSupportedFileTypes = new("All supported media files", exts.Remove(exts.Length - 1, 1));
        SupportedFileTypes.SupportedFileTypes.Insert(0, allSupportedFileTypes);
    }
    #endregion

}
