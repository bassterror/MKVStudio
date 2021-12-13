using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddTagATACommand : ICommand
    {
        private readonly TagsATAVM _tags;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public AddTagATACommand(TagsATAVM tags)
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
                    case ViewModelTypes.GobalTag:
                        _tags.GlobalTags.Add(new GlobalTagATAVM(_tags));
                        break;
                    case ViewModelTypes.TrackTag:
                        _tags.TrackTags.Add(new TrackTagATAVM(_tags));
                        break;
                }
            }
        }
    }
}
