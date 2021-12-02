using Microsoft.Win32;
using static MKVStudio.Services.UtilitiesService;

namespace MKVStudio.Services
{
    public interface IUtilitiesService
    {
        string GetExecutable(Executables executable);
        OpenFileDialog GetFileDialog(string filter, bool multiselect);
        string[] GetFilesFromFolder(string complexFilter);
    }
}