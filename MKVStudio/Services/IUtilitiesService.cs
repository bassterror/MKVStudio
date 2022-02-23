using Microsoft.Win32;
using MKVStudio.Models;
using System.Collections.ObjectModel;

namespace MKVStudio.Services;

public interface IUtilitiesService
{
    IExternalLibrariesService ExLib { get; }
    Preferences Preferences { get; }
    OpenFileDialog GetFileDialog(string filter, bool multiselect = false);
    string[] GetFilesFromFolder(string complexFilter);
    string GetFolder();
    string ConvertBytes(long value, int decimalPlaces = 1);
    void SavePreferences();
}
