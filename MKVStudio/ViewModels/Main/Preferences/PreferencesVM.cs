using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using MKVStudio.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class PreferencesVM : BaseViewModel
{
    public IUtilitiesService Util { get; }
    public MainVM Main { get; }
    public PreferencesV PreferencesV { get; }

    #region Navigation
    public BaseViewModel SelectedPreferencesTab { get; set; }
    private OftenUsedVM OftenUsed => new(Util);
    private DestinationVM OutputName => new(Util);
    public ICommand UpdateSelectedPreferencesTab => new UpdateSelectedPreferencesTabCommand(this, OftenUsed, OutputName);
    #endregion

    public PreferencesVM(MainVM main, IUtilitiesService util, PreferencesV preferencesV)
    {
        Main = main;
        Util = util;
        PreferencesV = preferencesV;
        UpdateSelectedPreferencesTab.Execute(ViewModelTypes.OftenUsed);
    }
}
