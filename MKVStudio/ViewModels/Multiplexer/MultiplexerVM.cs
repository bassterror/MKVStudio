using MKVStudio.Commands;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class MultiplexerVM : BaseViewModel
    {
        public IExternalLibrariesService ExLib { get; }
        public ObservableCollection<MultiplexVM> Multiplexes { get; set; } = new();
        public MultiplexVM SelectedMultiplex { get; set; }
        public ICommand AddFiles => new AddFilesCommand(this, ExLib);
        public ICommand AddFilesFromFolder => new AddFilesFromFolderCommand(this, ExLib);
        public ICommand ClearFiles => new ClearFilesCommand(this);
        public ICommand BatchEdit => new BatchEditCommand(this, ExLib);

        public MultiplexerVM(IExternalLibrariesService exLib)
        {
            ExLib = exLib;
        }
    }
}
