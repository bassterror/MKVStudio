using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveAllAttachmentsATACommand : ICommand
    {
        private readonly ObservableCollection<AttachmentATAVM> _attachments;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveAllAttachmentsATACommand(ObservableCollection<AttachmentATAVM> attachments)
        {
            _attachments = attachments;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _attachments.Clear();
        }
    }
}
