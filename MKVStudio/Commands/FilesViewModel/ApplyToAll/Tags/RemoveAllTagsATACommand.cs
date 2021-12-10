using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveAllTagsATACommand : ICommand
    {
        private readonly TagsATAVM _tags;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveAllTagsATACommand(TagsATAVM tags)
        {
            _tags = tags;
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
                    case ViewModelTypes.Tags:
                        _tags.GlobalTags.Clear();
                        _tags.TrackTags.Clear();
                        break;
                    case ViewModelTypes.GobalTag:
                        _tags.GlobalTags.Clear();
                        break;
                    case ViewModelTypes.TrackTag:
                        _tags.TrackTags.Clear();
                        break;
                }
            }
        }
    }
}
