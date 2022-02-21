using MKVStudio.ViewModels;

namespace MKVStudio.Commands;

public class UpdateSelectedMainTabCommand : BaseCommand
{
    private readonly MainVM _main;
    private readonly MultiplexerVM _multiplexer;
    private readonly JobQueueVM _jobQueue;

    public UpdateSelectedMainTabCommand(MainVM main, MultiplexerVM multiplexer, JobQueueVM jobQueue)
    {
        _main = main;
        _multiplexer = multiplexer;
        _jobQueue = jobQueue;
    }

    public override void Execute(object parameter)
    {
        if (parameter is ViewModelTypes viewModel)
        {
            SelectMainTab(viewModel);
        }
    }

    private void SelectMainTab(ViewModelTypes viewModel)
    {
        switch (viewModel)
        {
            case ViewModelTypes.Multiplexer:
                _main.SelectedMainTab = _multiplexer;
                break;
            case ViewModelTypes.JobQueue:
                _main.SelectedMainTab = _jobQueue;
                break;
        }
    }
}
