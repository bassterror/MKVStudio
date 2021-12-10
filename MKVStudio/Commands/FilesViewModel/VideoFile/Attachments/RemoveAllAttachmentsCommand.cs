using MKVStudio.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveAllAttachmentsCommand : ICommand
    {
        private readonly AttachmentsVM _attachments;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveAllAttachmentsCommand(AttachmentsVM attachments)
        {
            _attachments = attachments;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _attachments.Attachments.Clear();
        }
    }
}
