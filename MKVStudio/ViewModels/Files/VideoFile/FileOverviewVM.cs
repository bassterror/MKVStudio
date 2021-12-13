using MKVStudio.Commands;
using MKVStudio.Services;
using System.IO;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class FileOverviewVM : BaseViewModel
    {
        public string InputPath { get; private set; }
        public string InputName { get; private set; }
        public string InputExtension { get; private set; }
        public string InputFullName => InputName + InputExtension;
        public string InputFullPath { get; private set; }
        public string OutputPath { get; set; }
        public string OutputName { get; set; }
        public static string OutputExtension => ".mkv";
        public string OutputFullName => $"{OutputName}.{OutputExtension}";
        public string OutputFullPath => Path.Combine(OutputPath, OutputFullName);
        public ICommand Browse { get; set; }

        public FileOverviewVM(string source, IExternalLibrariesService exLib)
        {
            InputPath = Path.GetDirectoryName(source);
            InputName = Path.GetFileNameWithoutExtension(source);
            InputExtension = Path.GetExtension(source);
            InputFullPath = source;
            OutputPath = InputPath;
            OutputName = InputName + " - edit";
            Browse = new BrowseCommand(this, exLib);
        }
    }
}
