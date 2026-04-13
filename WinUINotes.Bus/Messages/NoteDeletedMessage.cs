using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;
using WinUINotes.Bus.Models;

namespace WinUINotes.Bus.Messages
{
    public class NoteDeletedMessage : ValueChangedMessage<Note>
    {
        public NoteDeletedMessage(Note note) : base(note)
        {
        }
    }
}
