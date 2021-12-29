﻿using MKVStudio.Commands;
using MKVStudio.Services;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class MultiplexVM : BaseViewModel
{

    public MultiplexerVM Multiplexer { get; }
    public IExternalLibrariesService ExLib { get; }
    public BaseViewModel SelectedMultiplexTab { get; set; }
    public ICommand UpdateMultiplexTab => new UpdateMultiplexTabCommand(this);

    public string PrimarySourceFullPath { get; set; }
    public string PrimarySourcePath { get; set; }
    public string PrimarySourceName { get; set; }
    private bool _isChecked;
    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            _isChecked = value;
            Multiplexer.IsCheckAll = Multiplexer.Multiplexes.Where(m => m.IsChecked).Count() != Multiplexer.Multiplexes.Count;
            Multiplexer.IsUncheckAll = Multiplexer.Multiplexes.Where(m => !m.IsChecked).Count() != Multiplexer.Multiplexes.Count;
        }
    }


    public InputVM Input => new(this, ExLib);
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

        UpdateMultiplexTab.Execute(ViewModelTypes.Input);
    }
}
