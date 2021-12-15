using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveAttachmentCommand : ICommand
    {
        private readonly AttachmentsVM _attachments;
        private readonly AttachmentVM _attachment;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveAttachmentCommand(AttachmentsVM attachments, AttachmentVM attachment)
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
