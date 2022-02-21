using MKVStudio.Commands;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class MainVM : BaseViewModel
{
    public IUtilitiesService Util { get; }

    #region Navigation
    public BaseViewModel SelectedMainTab { get; set; }
    private MultiplexerVM Multiplexer => new(Util);
    private JobQueueVM JobQueue => new();
    public ICommand UpdateSelectedMainTab => new UpdateSelectedMainTabCommand(this, Multiplexer, JobQueue);
    public ICommand Preferences => new PreferencesCommand(this, Util);
    #endregion

    public MainVM(IUtilitiesService util)
    {
        Util = util;
        UpdateSelectedMainTab.Execute(ViewModelTypes.Multiplexer);
    }
}
