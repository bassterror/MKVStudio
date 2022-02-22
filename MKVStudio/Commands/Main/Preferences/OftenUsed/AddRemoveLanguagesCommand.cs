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
                _prefLang.Util.Settings.PreferedLanguages.Add(_prefLang.SelectedAvailableLanguage);
                _prefLang.Util.Settings.PreferedLanguages = new ObservableCollection<Language>(_prefLang.Util.Settings.PreferedLanguages.OrderBy(l => l.Name));
                _prefLang.Util.Settings.AllLanguages.Remove(_prefLang.SelectedAvailableLanguage);
                _prefLang.Util.Settings.AllLanguages = new ObservableCollection<Language>(_prefLang.Util.Settings.AllLanguages.OrderBy(l => l.Name));
                _prefLang.PreferedLanguages = _prefLang.Util.Settings.PreferedLanguages;
                _prefLang.AvailableLanguages = _prefLang.Util.Settings.AllLanguages;
                _prefLang.Util.Settings.SetPreferedLanguages(GetPrefLangString());
                break;
            case "remove":
                _prefLang.SearchUserLanguage = string.Empty;
                _prefLang.Util.Settings.AllLanguages.Add(_prefLang.SelectedUserLanguage);
                _prefLang.Util.Settings.AllLanguages = new ObservableCollection<Language>(_prefLang.Util.Settings.AllLanguages.OrderBy(l => l.Name));
                _prefLang.Util.Settings.PreferedLanguages.Remove(_prefLang.SelectedUserLanguage);
                _prefLang.Util.Settings.PreferedLanguages = new ObservableCollection<Language>(_prefLang.Util.Settings.PreferedLanguages.OrderBy(l => l.Name));
                _prefLang.AvailableLanguages = _prefLang.Util.Settings.AllLanguages;
                _prefLang.PreferedLanguages = _prefLang.Util.Settings.PreferedLanguages;
                _prefLang.Util.Settings.SetPreferedLanguages(GetPrefLangString());
                break;
        }
    }

    private string GetPrefLangString()
    {
        string value = string.Empty;
        foreach (Language language in _prefLang.Util.Settings.PreferedLanguages)
        {
            value += $"{language.ID}|";
        }
        return value.Remove(value.Length - 1, 1);
    }
}
