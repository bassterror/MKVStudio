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
                    _preferences.ExLib.Languages.Add(_preferences.SelectedAvailableLanguage);
                    _preferences.ExLib.Languages = new ObservableCollection<Language>(_preferences.ExLib.Languages.OrderBy(l => l.Name));
                    _preferences.ExLib.AllLanguages.Remove(_preferences.SelectedAvailableLanguage);
                    _preferences.ExLib.AllLanguages = new ObservableCollection<Language>(_preferences.ExLib.AllLanguages.OrderBy(l => l.Name));
                    _preferences.UserLanguages = _preferences.ExLib.Languages;
                    _preferences.AvailableLanguages = _preferences.ExLib.AllLanguages;
                    _preferences.ExLib.Util.SetPreferedLanguages(GetPrefLangString());
                    break;
                case "remove":
                    _preferences.SearchUserLanguage = string.Empty;
                    _preferences.ExLib.AllLanguages.Add(_preferences.SelectedUserLanguage);
                    _preferences.ExLib.AllLanguages = new ObservableCollection<Language>(_preferences.ExLib.AllLanguages.OrderBy(l => l.Name));
                    _preferences.ExLib.Languages.Remove(_preferences.SelectedUserLanguage);
                    _preferences.ExLib.Languages = new ObservableCollection<Language>(_preferences.ExLib.Languages.OrderBy(l => l.Name));
                    _preferences.AvailableLanguages = _preferences.ExLib.AllLanguages;
                    _preferences.UserLanguages = _preferences.ExLib.Languages;
                    _preferences.ExLib.Util.SetPreferedLanguages(GetPrefLangString());
                    break;
            }
        }
    }

    private string GetPrefLangString()
    {
        string value = string.Empty;
        foreach (Language language in _preferences.ExLib.Languages)
        {
            value += $"{language.ID}|";
        }
        return value.Remove(value.Length - 1, 1);
    }
}
