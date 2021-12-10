using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveAttachmentATACommand : ICommand
    {
        private readonly AttachmentsATAVM _attachments;
        private readonly AttachmentATAVM _attachment;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveAttachmentATACommand(AttachmentsATAVM attachments, AttachmentATAVM attachment)
        {
            _attachments = attachments;
            _attachment = attachment;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _attachments.Attachments.Remove(_attachment);
        }
    }
}
