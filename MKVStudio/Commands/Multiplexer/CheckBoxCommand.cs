using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands;

public class CheckBoxCommand : ICommand
{
    private readonly object _collection;

    public event EventHandler CanExecuteChanged { add { } remove { } }

    public CheckBoxCommand(object collection)
    {
        _collection = collection;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        if (parameter is bool value)
        {
            if (_collection is ObservableCollection<MultiplexVM> multiplexes)
            {
                foreach (MultiplexVM multiplex in multiplexes)
                {
                    multiplex.IsChecked = value;
                }
            }
            if (_collection is ObservableCollection<TrackVM> tracks)
            {
                foreach (TrackVM track in tracks)
                {
                    track.IsChecked = value;
                }
            }
            if (_collection is ObservableCollection<AttachmentVM> attachments)
            {
                foreach (AttachmentVM attachment in attachments)
                {
                    attachment.IsChecked = value;
                }
            }
        }
    }
}
