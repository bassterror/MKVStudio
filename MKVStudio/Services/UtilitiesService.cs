using Microsoft.Win32;
using MKVStudio.Models;
using System;
using System.Collections;
using System.IO;
using F = System.Windows.Forms;

namespace MKVStudio.Services;

public class UtilitiesService : IUtilitiesService
{
    public IExternalLibrariesService ExLib { get; }
    public Settings Settings { get; set; }

    public UtilitiesService(IExternalLibrariesService exLib)
    {
        ExLib = exLib;
        Settings = new(this);
    }

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
