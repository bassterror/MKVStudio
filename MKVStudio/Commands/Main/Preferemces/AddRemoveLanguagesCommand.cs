using MKVStudio.Models;
using MKVStudio.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace MKVStudio.Commands;

public class AddRemoveLanguagesCommand : BaseCommand
{
    private readonly PreferencesVM _preferences;

    public AddRemoveLanguagesCommand(PreferencesVM preferences)
    {
        _preferences = preferences;
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
                _preferences.SearchAvailableLanguage = string.Empty;
                _preferences.Util.Settings.PreferedLanguages.Add(_preferences.SelectedAvailableLanguage);
                _preferences.Util.Settings.PreferedLanguages = new ObservableCollection<Language>(_preferences.Util.Settings.PreferedLanguages.OrderBy(l => l.Name));
                _preferences.Util.Settings.AllLanguages.Remove(_preferences.SelectedAvailableLanguage);
                _preferences.Util.Settings.AllLanguages = new ObservableCollection<Language>(_preferences.Util.Settings.AllLanguages.OrderBy(l => l.Name));
                _preferences.UserLanguages = _preferences.Util.Settings.PreferedLanguages;
                _preferences.AvailableLanguages = _preferences.Util.Settings.AllLanguages;
                _preferences.Util.Settings.SetPreferedLanguages(GetPrefLangString());
                break;
            case "remove":
                _preferences.SearchUserLanguage = string.Empty;
                _preferences.Util.Settings.AllLanguages.Add(_preferences.SelectedUserLanguage);
                _preferences.Util.Settings.AllLanguages = new ObservableCollection<Language>(_preferences.Util.Settings.AllLanguages.OrderBy(l => l.Name));
                _preferences.Util.Settings.PreferedLanguages.Remove(_preferences.SelectedUserLanguage);
                _preferences.Util.Settings.PreferedLanguages = new ObservableCollection<Language>(_preferences.Util.Settings.PreferedLanguages.OrderBy(l => l.Name));
                _preferences.AvailableLanguages = _preferences.Util.Settings.AllLanguages;
                _preferences.UserLanguages = _preferences.Util.Settings.PreferedLanguages;
                _preferences.Util.Settings.SetPreferedLanguages(GetPrefLangString());
                break;
        }
    }

    private string GetPrefLangString()
    {
        string value = string.Empty;
        foreach (Language language in _preferences.Util.Settings.PreferedLanguages)
        {
            value += $"{language.ID}|";
        }
        return value.Remove(value.Length - 1, 1);
    }
}
