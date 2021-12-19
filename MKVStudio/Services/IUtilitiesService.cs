using Microsoft.Win32;
using static MKVStudio.Services.UtilitiesService;

namespace MKVStudio.Services
{
    public interface IUtilitiesService
    {
        string GetExecutable(Executables executable);
        OpenFileDialog GetFileDialog(string filter, bool multiselect);
        /// <summary>
        /// Gets files from selected folder
        /// </summary>
        /// <returns>Array of file paths</returns>
        string[] GetFilesFromFolder(string complexFilter);
        string GetFolder();
        string ConvertBytes(long value, int decimalPlaces = 1);
    }
}