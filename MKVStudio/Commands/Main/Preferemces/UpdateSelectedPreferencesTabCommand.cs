using MKVStudio.ViewModels;

namespace MKVStudio.Commands;

public class UpdateSelectedPreferencesTabCommand : BaseCommand
{
    private readonly PreferencesVM _preferences;
    private readonly OftenUsedVM _oftenUsed;

    public UpdateSelectedPreferencesTabCommand(PreferencesVM preferences, OftenUsedVM oftenUsed)
    {
        _preferences = preferences;
        _oftenUsed = oftenUsed;
    }

    public override void Execute(object parameter)
    {
        if (parameter is ViewModelTypes viewModel)
        {
            SelectPreferencesTab(viewModel);
        }
    }
    private void SelectPreferencesTab(ViewModelTypes viewModel)
    {
        switch (viewModel)
        {
            case ViewModelTypes.OftenUsed:
                _preferences.SelectedPreferencesTab = _oftenUsed;
                break;
        }
    }
}
