using MKVStudio.Models;
using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MKVStudio.Commands;

public class AddRemoveLanguagesCommand : ICommand
{
    private readonly PreferencesVM _preferences;

    public event EventHandler CanExecuteChanged { add { } remove { } }

    public AddRemoveLanguagesCommand(PreferencesVM preferences)
    {
        _preferences = preferences;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        if (parameter is string value)
        {
            switch (value)
            {
                case "add":
                    _preferences.SearchAvailableLanguage = string.Empty;
                    _preferences.Util.Languages.Add(_preferences.SelectedAvailableLanguage);
                    _preferences.Util.Languages = new ObservableCollection<Language>(_preferences.Util.Languages.OrderBy(l => l.Name));
                    _preferences.Util.AllLanguages.Remove(_preferences.SelectedAvailableLanguage);
                    _preferences.Util.AllLanguages = new ObservableCollection<Language>(_preferences.Util.AllLanguages.OrderBy(l => l.Name));
                    _preferences.UserLanguages = _preferences.Util.Languages;
                    _preferences.AvailableLanguages = _preferences.Util.AllLanguages;
                    _preferences.Util.SetPreferedLanguages(GetPrefLangString());
                    break;
                case "remove":
                    _preferences.SearchUserLanguage = string.Empty;
                    _preferences.Util.AllLanguages.Add(_preferences.SelectedUserLanguage);
                    _preferences.Util.AllLanguages = new ObservableCollection<Language>(_preferences.Util.AllLanguages.OrderBy(l => l.Name));
                    _preferences.Util.Languages.Remove(_preferences.SelectedUserLanguage);
                    _preferences.Util.Languages = new ObservableCollection<Language>(_preferences.Util.Languages.OrderBy(l => l.Name));
                    _preferences.AvailableLanguages = _preferences.Util.AllLanguages;
                    _preferences.UserLanguages = _preferences.Util.Languages;
                    _preferences.Util.SetPreferedLanguages(GetPrefLangString());
                    break;
            }
        }
    }

    private string GetPrefLangString()
    {
        string value = string.Empty;
        foreach (Language language in _preferences.Util.Languages)
        {
            value += $"{language.ID}|";
        }
        return value.Remove(value.Length - 1, 1);
    }
}
