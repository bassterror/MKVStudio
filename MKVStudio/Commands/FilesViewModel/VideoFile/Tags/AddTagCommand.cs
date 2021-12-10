using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddTagCommand : ICommand
    {
        private readonly TagsVM _tags;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public AddTagCommand(TagsVM tags)
        {
            _tags = tags;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewModelTypes viewModelType)
            {
                switch (viewModelType)
                {
                    case ViewModelTypes.GobalTag:
                        _tags.GlobalTags.Add(new GlobalTagVM(_tags));
                        break;
                    case ViewModelTypes.TrackTag:
                        _tags.TrackTags.Add(new TrackTagVM(_tags));
                        break;
                }
            }
        }
    }
}
