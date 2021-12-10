using MKVStudio.Commands;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class FileOverviewATAVM : BaseViewModel
    {
        private readonly IExternalLibrariesService _exLib;

        public string OutputPath { get; set; }
        public string OutputName { get; set; }
        public ICommand Browse => new BrowseATACommand(this, _exLib);

        public FileOverviewATAVM(IExternalLibrariesService exLib)
        {
            _exLib = exLib;
        }
    }
}
