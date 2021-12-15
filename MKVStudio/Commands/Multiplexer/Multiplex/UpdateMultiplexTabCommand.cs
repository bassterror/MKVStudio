using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class UpdateMultiplexTabCommand : ICommand
    {
        public event EventHandler CanExecuteChanged { add { } remove { } }
        private readonly MultiplexVM _multiplex;
        private readonly OutputVM _output;
        private readonly InputVM _input;
        private readonly ChaptersVM _chapters;
        private readonly AttachmentsVM _attachments;

        public UpdateMultiplexTabCommand(MultiplexVM multiplex,
            InputVM input,
            OutputVM output,
            ChaptersVM chapters,
            AttachmentsVM attachments)
        {
            _multiplex = multiplex;
            _input = input;
            _output = output;
            _chapters = chapters;
            _attachments = attachments;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewModelTypes viewModel)
            {
                switch (viewModel)
                {
                    case ViewModelTypes.Input:
                        _multiplex.SelectedMultiplexTab = _input;
                        break;
                    case ViewModelTypes.Output:
                        _multiplex.SelectedMultiplexTab = _output;
                        break;
                    case ViewModelTypes.Chapters:
                        _multiplex.SelectedMultiplexTab = _chapters;
                        break;
                    case ViewModelTypes.Attachments:
                        _multiplex.SelectedMultiplexTab = _attachments;
                        break;
                }
            }
        }
    }
}
