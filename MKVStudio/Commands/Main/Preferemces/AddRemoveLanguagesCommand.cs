using MKVStudio.Models;
using MKVStudio.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace MKVStudio.Commands;

public class AddRemoveLanguagesCommand : BaseCommand
{
    private readonly OftenUsedVM _oftenUsed;

    public AddRemoveLanguagesCommand(OftenUsedVM oftenUsed)
    {
        _oftenUsed = oftenUsed;
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
                _oftenUsed.SearchAvailableLanguage = string.Empty;
                _oftenUsed.Util.Settings.PreferedLanguages.Add(_oftenUsed.SelectedAvailableLanguage);
                _oftenUsed.Util.Settings.PreferedLanguages = new ObservableCollection<Language>(_oftenUsed.Util.Settings.PreferedLanguages.OrderBy(l => l.Name));
                _oftenUsed.Util.Settings.AllLanguages.Remove(_oftenUsed.SelectedAvailableLanguage);
                _oftenUsed.Util.Settings.AllLanguages = new ObservableCollection<Language>(_oftenUsed.Util.Settings.AllLanguages.OrderBy(l => l.Name));
                _oftenUsed.PreferedLanguages = _oftenUsed.Util.Settings.PreferedLanguages;
                _oftenUsed.AvailableLanguages = _oftenUsed.Util.Settings.AllLanguages;
                _oftenUsed.Util.Settings.SetPreferedLanguages(GetPrefLangString());
                break;
            case "remove":
                _oftenUsed.SearchUserLanguage = string.Empty;
                _oftenUsed.Util.Settings.AllLanguages.Add(_oftenUsed.SelectedUserLanguage);
                _oftenUsed.Util.Settings.AllLanguages = new ObservableCollection<Language>(_oftenUsed.Util.Settings.AllLanguages.OrderBy(l => l.Name));
                _oftenUsed.Util.Settings.PreferedLanguages.Remove(_oftenUsed.SelectedUserLanguage);
                _oftenUsed.Util.Settings.PreferedLanguages = new ObservableCollection<Language>(_oftenUsed.Util.Settings.PreferedLanguages.OrderBy(l => l.Name));
                _oftenUsed.AvailableLanguages = _oftenUsed.Util.Settings.AllLanguages;
                _oftenUsed.PreferedLanguages = _oftenUsed.Util.Settings.PreferedLanguages;
                _oftenUsed.Util.Settings.SetPreferedLanguages(GetPrefLangString());
                break;
        }
    }

    private string GetPrefLangString()
    {
        string value = string.Empty;
        foreach (Language language in _oftenUsed.Util.Settings.PreferedLanguages)
        {
            value += $"{language.ID}|";
        }
        return value.Remove(value.Length - 1, 1);
    }
}
