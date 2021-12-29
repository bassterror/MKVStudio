using Microsoft.Win32;
using static MKVStudio.Services.UtilitiesService;

namespace MKVStudio.Services;

public interface IUtilitiesService
{
    string GetExecutable(Executables executable);
    void SetPreferedLanguages(string languages);
    string GetPreferedLanguages();
    OpenFileDialog GetFileDialog(string filter, bool multiselect);
    string[] GetFilesFromFolder(string complexFilter);
    string GetFolder();
    string ConvertBytes(long value, int decimalPlaces = 1);
}
