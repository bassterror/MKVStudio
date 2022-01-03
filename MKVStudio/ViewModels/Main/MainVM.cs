using MKVStudio.Commands;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class MainVM : BaseViewModel
{
    public BaseViewModel SelectedMainTab { get; set; }
    private MultiplexerVM Multiplexer => new(Util);
    private JobQueueVM JobQueue => new();

    public ICommand UpdateSelectedMainTab => new UpdateSelectedMainTabCommand(this, Multiplexer, JobQueue);
    public IUtilitiesService Util { get; }
    public ICommand Preferences => new PreferencesCommand(this, Util);

    public MainVM(IUtilitiesService util)
    {
        Util = util;
        UpdateSelectedMainTab.Execute(ViewModelTypes.Multiplexer);
    }
}
