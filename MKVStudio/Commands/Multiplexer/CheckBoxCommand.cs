using MKVStudio.Models;
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
                CheckUncheckMultiplexes(value, multiplexes);
            }
            if (_collection is ObservableCollection<TrackVM> tracks)
            {
                CheckUncheckTracks(value, tracks);
            }
            if (_collection is ObservableCollection<Attachment> attachments)
            {
                CheckUncheckAttachments(value, attachments);
            }
        }
    }

    private static void CheckUncheckMultiplexes(bool value, ObservableCollection<MultiplexVM> multiplexes)
    {
        foreach (MultiplexVM multiplex in multiplexes)
        {
            multiplex.IsChecked = value;
        }
    }

    private static void CheckUncheckTracks(bool value, ObservableCollection<TrackVM> tracks)
    {
        foreach (TrackVM track in tracks)
        {
            track.IsChecked = value;
        }
    }

    private static void CheckUncheckAttachments(bool value, ObservableCollection<Attachment> attachments)
    {
        foreach (Attachment attachment in attachments)
        {
            attachment.ToBeAdded = value;
        }
    }
}
