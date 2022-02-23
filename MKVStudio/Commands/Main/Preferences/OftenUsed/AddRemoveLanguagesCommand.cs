using MKVStudio.Models;
using MKVStudio.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace MKVStudio.Commands;

public class AddRemoveLanguagesCommand : BaseCommand
{
    private readonly PreferedLanguagesVM _prefLang;

    public AddRemoveLanguagesCommand(PreferedLanguagesVM prefLang)
    {
        _prefLang = prefLang;
    }

    public override void Execute(object parameter)
    {
        if (parameter is string value)
        {
            AddRemove(value);
        }
    }

    private void AddRemove(string value)
    {
        switch (value)
        {
            case "add":
                _prefLang.SearchAvailableLanguage = string.Empty;
                _prefLang.Util.Preferences.Languages.PreferedLanguages.Add(_prefLang.SelectedAvailableLanguage);
                _prefLang.Util.Preferences.Languages.PreferedLanguages = new ObservableCollection<Language>(_prefLang.Util.Preferences.Languages.PreferedLanguages.OrderBy(l => l.Name));
                _prefLang.Util.Preferences.Languages.AllLanguages.Remove(_prefLang.SelectedAvailableLanguage);
                _prefLang.Util.Preferences.Languages.AllLanguages = new ObservableCollection<Language>(_prefLang.Util.Preferences.Languages.AllLanguages.OrderBy(l => l.Name));
                break;
            case "remove":
                _prefLang.SearchUserLanguage = string.Empty;
                _prefLang.Util.Preferences.Languages.AllLanguages.Add(_prefLang.SelectedUserLanguage);
                _prefLang.Util.Preferences.Languages.AllLanguages = new ObservableCollection<Language>(_prefLang.Util.Preferences.Languages.AllLanguages.OrderBy(l => l.Name));
                _prefLang.Util.Preferences.Languages.PreferedLanguages.Remove(_prefLang.SelectedUserLanguage);
                _prefLang.Util.Preferences.Languages.PreferedLanguages = new ObservableCollection<Language>(_prefLang.Util.Preferences.Languages.PreferedLanguages.OrderBy(l => l.Name));
                break;
        }
        _prefLang.AvailableLanguages = _prefLang.Util.Preferences.Languages.AllLanguages;
        _prefLang.PreferedLanguages = _prefLang.Util.Preferences.Languages.PreferedLanguages;
        _prefLang.Util.SavePreferences();
    }
}
