using MKVStudio.Commands;
using MKVStudio.Services;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class OftenUsedVM : BaseViewModel
{
    public IUtilitiesService Util { get; }
    public BaseViewModel SelectedOftenUsedTab { get; set; }
    public PreferedLanguagesVM PreferedLanguages { get; set; }

    public ICommand UpdateOftenUsedTab => new UpdateOftenUsedTabCommand(this, PreferedLanguages);

    public OftenUsedVM(IUtilitiesService util)
    {
        Util = util;
        PreferedLanguages = new(util);
        UpdateOftenUsedTab.Execute(ViewModelTypes.PreferedLanguages);
    }
}
