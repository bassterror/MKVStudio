using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels;

public class OftenUsedVM : BaseViewModel
{
    public IUtilitiesService Util { get; }

    public ObservableCollection<Language> AvailableLanguages { get; set; }
    public Language SelectedAvailableLanguage { get; set; }
    private string _searchAvailableLanguage;
    public string SearchAvailableLanguage
    {
        get => _searchAvailableLanguage;
        set
        {
            _searchAvailableLanguage = value;
            AvailableLanguages = new ObservableCollection<Language>(
                Util.Settings.AllLanguages.Where(l => l.Name.Contains(value, System.StringComparison.OrdinalIgnoreCase)).ToList());
        }
    }

    public ObservableCollection<Language> PreferedLanguages { get; set; }
    public Language SelectedUserLanguage { get; set; }
    private string _searchUserLanguage;
    public string SearchUserLanguage
    {
        get => _searchUserLanguage;
        set
        {
            _searchUserLanguage = value;
            PreferedLanguages = new ObservableCollection<Language>(
                Util.Settings.PreferedLanguages.Where(l => l.Name.Contains(value, System.StringComparison.OrdinalIgnoreCase)).ToList());
        }
    }

    public ICommand AddRemoveLanguage => new AddRemoveLanguagesCommand(this);

    public OftenUsedVM(IUtilitiesService util)
    {
        Util = util;
        AvailableLanguages = Util.Settings.AllLanguages;
        PreferedLanguages = Util.Settings.PreferedLanguages;
    }
}
