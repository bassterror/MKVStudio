using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class MultiplexVM : BaseViewModel
{
    private readonly IUtilitiesService _util;
    private readonly MultiplexerVM _multiplexer;
    private bool _isChecked;

    public SourceFileInfo SourceFile { get; set; }
    public BaseViewModel SelectedMultiplexTab { get; set; }
    public ICommand UpdateMultiplexTab => new UpdateMultiplexTabCommand(this);
    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            _isChecked = value;
            _multiplexer.IsCheckAll = _multiplexer.Multiplexes.Where(m => m.IsChecked).Count() != _multiplexer.Multiplexes.Count;
            _multiplexer.IsUncheckAll = _multiplexer.Multiplexes.Where(m => !m.IsChecked).Count() != _multiplexer.Multiplexes.Count;
        }
    }
    public InputVM Input { get; set; }
    public OutputVM Output { get; set; }
    public AttachmentsVM Attachments { get; set; }
    public ChaptersVM Chapters { get; set; }

    public ICommand Browse => new BrowseCommand(this, _util);

    public MultiplexVM(IUtilitiesService util, MultiplexerVM multiplexer, SourceFileInfo sourceFile)
    {
        _util = util;
        _multiplexer = multiplexer;
        SourceFile = sourceFile;
        IsChecked = true;
        Input = new(_util, this);
        Output = new(_util, this);
        Attachments = new(_util);
        Chapters = new ChaptersVM();

        UpdateMultiplexTab.Execute(ViewModelTypes.Input);
    }
}
