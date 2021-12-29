using MKVStudio.Commands;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class MainVM : BaseViewModel
{
    public BaseViewModel SelectedMainTab { get; set; }
    private MultiplexerVM Multiplexer => new(ExLib);
    private JobQueueVM JobQueue => new();

    public ICommand UpdateSelectedMainTab => new UpdateSelectedMainTabCommand(this, Multiplexer, JobQueue);
    public IExternalLibrariesService ExLib { get; }
    public ICommand Preferences => new PreferencesCommand(this, ExLib);

    public MainVM(IExternalLibrariesService exLib)
    {
        ExLib = exLib;
        UpdateSelectedMainTab.Execute(ViewModelTypes.Multiplexer);
    }
}
