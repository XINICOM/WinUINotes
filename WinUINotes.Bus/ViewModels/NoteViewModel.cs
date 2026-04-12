using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WinUINotes.Bus.Models;

namespace WinUINotes.Bus.ViewModels
{
    public partial class NoteViewModel : ObservableObject
    {
        private Note note;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyCanExecuteChangedFor(nameof(DeleteCommand))]
        private string filename = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string text = string.Empty;

        [ObservableProperty]
        private DateTime date = DateTime.Now;

        public NoteViewModel()
        {
            this.note = new Note();
            this.Filename = note.Filename;
        }

        public void InitializeForExistingNote(Note note)
        {
            this.note = note;
            this.Filename = note.Filename;
            this.Text = note.Text;
            this.Date = note.Date;
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task Save()
        {
            note.Filename = this.Filename;
            note.Text = this.Text;
            note.Date = this.Date;
            await note.SaveAsync();
            
            DeleteCommand.NotifyCanExecuteChanged();
        }

        private bool CanSave()
        {
            return note is not null
                && !string.IsNullOrWhiteSpace(this.Text)
                && !string.IsNullOrWhiteSpace(this.Filename);
        }

        [RelayCommand(CanExecute = nameof(CanDelete))]
        private async Task Delete()
        {
            await note.DeleteAsync();
            note = new Note();
        }

        private bool CanDelete()
        {
            // Note: This is to illustrate how commands can be
            // enabled or disabled.
            // In a real application, you shouldn't perform
            // file operations in your CanExecute logic.
            return note is not null
                && !string.IsNullOrWhiteSpace(this.Filename);
                //TODO && this.note.NoteFileExists();
        }
    }
}
