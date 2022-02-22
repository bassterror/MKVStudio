using MKVStudio.ViewModels;

namespace MKVStudio.Commands;

public class UpdateSelectedPreferencesTabCommand : BaseCommand
{
    private readonly PreferencesVM _preferences;
    private readonly OftenUsedVM _oftenUsed;
    private readonly OutputNameVM _outputName;

    public UpdateSelectedPreferencesTabCommand(PreferencesVM preferences, OftenUsedVM oftenUsed, OutputNameVM outputName)
    {
        _preferences = preferences;
        _oftenUsed = oftenUsed;
        _outputName = outputName;
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
            case ViewModelTypes.OutputName:
                _preferences.SelectedPreferencesTab = _outputName;
                break;
        }
    }
}
