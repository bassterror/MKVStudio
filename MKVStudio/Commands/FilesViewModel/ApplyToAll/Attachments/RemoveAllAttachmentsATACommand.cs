using MKVStudio.ViewModels;
using System;
using System.Windows.Input;

namespace MKVStudio.Commands
{
    public class RemoveAllAttachmentsATACommand : ICommand
    {
        private readonly AttachmentsATAVM _attachments;

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public RemoveAllAttachmentsATACommand(AttachmentsATAVM attachments)
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
