using Microsoft.Win32;
using MKVStudio.Models;
using System.Collections.ObjectModel;
using static MKVStudio.Services.UtilitiesService;

namespace MKVStudio.Services;

public interface IUtilitiesService
{
    IExternalLibrariesService ExLib { get; }
    ObservableCollection<Language> AllLanguages { get; set; }
    ObservableCollection<Language> Languages { get; set; }
    SupportedFileTypesCollection SupportedFileTypesCollection { get; set; }
    MIMETypeCollection MIMETypes { get; set; }
    void SetPreferedLanguages(string languages);
    string GetPreferedLanguages();
    OpenFileDialog GetFileDialog(string filter, bool multiselect);
    string[] GetFilesFromFolder(string complexFilter);
    string GetFolder();
    string ConvertBytes(long value, int decimalPlaces = 1);
}
