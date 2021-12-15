using MKVStudio.Commands;
using MKVStudio.Services;
using System.IO;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class MultiplexVM : BaseViewModel
    {
        public MultiplexerVM Multiplexer { get; }
        public IExternalLibrariesService ExLib { get; }
        public BaseViewModel SelectedMultiplexTab { get; set; }
        public ICommand UpdateMultiplexTab => new UpdateMultiplexTabCommand(this, Input, Output, Chapters, Attachments);

        public string PrimarySourceFullPath { get; set; }
        public string PrimarySourcePath { get; set; }
        public string PrimarySourceName { get; set; }
        public bool IsChecked { get; set; }


        public InputVM Input { get; set; }
        public OutputVM Output { get; set; }
        public AttachmentsVM Attachments { get; set; }
        public ChaptersVM Chapters { get; set; }

        public ICommand Browse => new BrowseCommand(this, ExLib);

        public MultiplexVM(MultiplexerVM multiplexer, string source, IExternalLibrariesService exLib)
        {
            Multiplexer = multiplexer;
            ExLib = exLib;
            PrimarySourceFullPath = source;
            PrimarySourcePath = Path.GetDirectoryName(source);
            PrimarySourceName = Path.GetFileName(source);
            IsChecked = true;
            Input = new InputVM(this, exLib);

            UpdateMultiplexTab.Execute(ViewModelTypes.Input);
        }
    }
}
