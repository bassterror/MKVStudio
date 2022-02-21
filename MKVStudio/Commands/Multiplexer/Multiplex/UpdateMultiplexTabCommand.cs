using MKVStudio.ViewModels;

namespace MKVStudio.Commands;

public class UpdateMultiplexTabCommand : BaseCommand
{
    private readonly MultiplexVM _multiplex;

    public UpdateMultiplexTabCommand(MultiplexVM multiplex)
    {
        _multiplex = multiplex;
    }

    public override void Execute(object parameter)
    {
        if (parameter is ViewModelTypes viewModel)
        {
            SelectMultiplexTab(viewModel);
        }
    }

    private void SelectMultiplexTab(ViewModelTypes viewModel)
    {
        switch (viewModel)
        {
            case ViewModelTypes.Input:
                _multiplex.SelectedMultiplexTab = _multiplex.Input;
                break;
            case ViewModelTypes.Output:
                _multiplex.SelectedMultiplexTab = _multiplex.Output;
                break;
            case ViewModelTypes.Attachments:
                _multiplex.SelectedMultiplexTab = _multiplex.Attachments;
                break;
            case ViewModelTypes.Chapters:
                _multiplex.SelectedMultiplexTab = _multiplex.Chapters;
                break;
        }
    }
}
