using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddAttachmentATACommand : ICommand
    {
        private readonly AttachmentsATAVM _attachments;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public AddAttachmentATACommand(AttachmentsATAVM attachments)
        {
            _attachments = attachments;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _attachments.Attachments.Add(new AttachmentATAVM(_attachments));
        }
    }
}
