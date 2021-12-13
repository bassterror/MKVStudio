using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveTagCommand : ICommand
    {
        private readonly TagsVM _tags;
        private readonly GlobalTagVM _globalTag;
        private readonly TrackTagVM _trackTag;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveTagCommand(TagsVM tags,
            GlobalTagVM globalTag = null,
            TrackTagVM trackTag = null)
        {
            _tags = tags;
            _globalTag = globalTag;
            _trackTag = trackTag;
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
                    case ViewModelTypes.GobalTag:
                        _tags.GlobalTags.Remove(_globalTag);
                        break;
                    case ViewModelTypes.TrackTag:
                        _tags.TrackTags.Remove(_trackTag);
                        break;
                }
            }
        }
    }
}
