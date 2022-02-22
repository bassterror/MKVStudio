using MKVStudio.ViewModels;

namespace MKVStudio.Commands;

public class UpdateOftenUsedTabCommand : BaseCommand
{
    private readonly OftenUsedVM _oftenUsed;
    private readonly PreferedLanguagesVM _prefLang;

    public UpdateOftenUsedTabCommand(OftenUsedVM oftenUsed, PreferedLanguagesVM prefLang)
    {
        _oftenUsed = oftenUsed;
        _prefLang = prefLang;
    }

    public override void Execute(object parameter)
    {
        if (parameter is ViewModelTypes viewModel)
        {
            SelectOftenUsedTab(viewModel);
        }
    }

    private void SelectOftenUsedTab(ViewModelTypes viewModel)
    {
        switch (viewModel)
        {
            case ViewModelTypes.PreferedLanguages:
                _oftenUsed.SelectedOftenUsedTab = _prefLang;
                break;
        }
    }
}
