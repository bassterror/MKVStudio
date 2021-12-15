using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class AddAttachmentCommand : ICommand
    {
        private readonly AttachmentsVM _attachmentsVM;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public AddAttachmentCommand(AttachmentsVM attachmentsVM)
        {
            _attachmentsVM = attachmentsVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _attachmentsVM.Attachments.Add(new AttachmentVM(_attachmentsVM));
        }
    }
}
