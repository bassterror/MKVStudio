using MKVStudio.Commands;
using MKVStudio.Services;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class MultiplexVM : BaseViewModel
{
    private bool _isChecked;

    public MultiplexerVM Multiplexer { get; }
    public IUtilitiesService Util { get; }
    public BaseViewModel SelectedMultiplexTab { get; set; }
    public ICommand UpdateMultiplexTab => new UpdateMultiplexTabCommand(this);

    public string PrimarySourceFullPath { get; set; }
    public string PrimarySourcePath { get; set; }
    public string PrimarySourceName { get; set; }
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


    public InputVM Input { get; set; }
    public OutputVM Output { get; set; }
    public AttachmentsVM Attachments { get; set; }
    public ChaptersVM Chapters { get; set; }

    public ICommand Browse => new BrowseCommand(this, Util);

    public MultiplexVM(MultiplexerVM multiplexer, string source, IUtilitiesService util)
    {
        Multiplexer = multiplexer;
        Util = util;
        PrimarySourceFullPath = source;
        PrimarySourcePath = Path.GetDirectoryName(source);
        PrimarySourceName = Path.GetFileName(source);
        IsChecked = true;

        Input = new(this, Util);
        Attachments = new(Util);

        UpdateMultiplexTab.Execute(ViewModelTypes.Input);
    }
}
