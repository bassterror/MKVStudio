﻿using MKVStudio.Commands;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class MultiplexVM : BaseViewModel
    {
        public MultiplexerVM Multiplexer { get; }
        public IExternalLibrariesService ExLib { get; }
        public ICommand RemoveFile => new RemoveFileCommand(Multiplexer, this);
        public BaseViewModel SelectedMultiplexTab { get; set; }
        public ICommand UpdateMultiplexTab => new UpdateMultiplexTabCommand(this, Input, Output, Chapters, Attachments);

        public string PrimarySourcePath { get; set; }
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
            PrimarySourcePath = source;
            IsChecked = true;
            Input = new InputVM(this, exLib);

            UpdateMultiplexTab.Execute(ViewModelTypes.Input);
        }
    }
}