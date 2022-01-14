using MKVStudio.ViewModels;
using System.Collections.ObjectModel;

namespace MKVStudio.Commands;

public class CheckBoxCommand : BaseCommand
{
    private readonly object _collection;

    public CheckBoxCommand(object collection)
    {
        _collection = collection;
    }

    public override void Execute(object parameter)
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
