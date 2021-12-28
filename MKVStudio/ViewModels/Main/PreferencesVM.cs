using MKVStudio.Commands;
using MKVStudio.Models;
using MKVStudio.Services;
using MKVStudio.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.ViewModels
{
    public class PreferencesVM : BaseViewModel
    {

        public MainVM Main { get; }
        public IExternalLibrariesService ExLib { get; }
        public PreferencesV PreferencesV { get; }

        public ObservableCollection<Language> AvailableLanguages { get; set; }
        public Language SelectedAvailableLanguage { get; set; }
        private string _searchAvailableLanguage;
        public string SearchAvailableLanguage
        {
            get => _searchAvailableLanguage;
            set
            {
                _searchAvailableLanguage = value;
                AvailableLanguages = new ObservableCollection<Language>(ExLib.AllLanguages.Where(l => l.Name.Contains(value, System.StringComparison.OrdinalIgnoreCase)).ToList());
            }
        }

        public ObservableCollection<Language> UserLanguages { get; set; }
        public Language SelectedUserLanguage { get; set; }
        private string _searchUserLanguage;
        public string SearchUserLanguage
        {
            get => _searchUserLanguage;
            set
            {
                _searchUserLanguage = value;
                UserLanguages = new ObservableCollection<Language>(ExLib.Languages.Where(l => l.Name.Contains(value, System.StringComparison.OrdinalIgnoreCase)).ToList());
            }
        }

        public ICommand AddRemoveLanguage => new AddRemoveLanguagesCommand(this);

        public PreferencesVM(MainVM main, IExternalLibrariesService exLib, PreferencesV preferencesV)
        {
            Main = main;
            ExLib = exLib;
            PreferencesV = preferencesV;
            AvailableLanguages = ExLib.AllLanguages;
            UserLanguages = ExLib.Languages;
        }
    }
}
