using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WinUINotes.Bus.Services;

namespace WinUINotes.Bus.Models
{
    class AllNotes
    {
        private IFileService fileService;
        public ObservableCollection<Note> Notes { get; set; } = [];

        public AllNotes(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public async Task LoadNotes()
        {
            Notes.Clear();
            // Get the folder where the notes are stored.
            //StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            //await GetFilesInFolderAsync(storageFolder);
            await GetFilesInFolderAsync(fileService.GetLocalFolder());
        }

        private async Task GetFilesInFolderAsync(IStorageFolder folder)
        {
            // Each StorageItem can be either a folder or a file.
            IReadOnlyList<IStorageItem> storageItems = await fileService.GetStorageItemsAsync(folder);
            foreach (IStorageItem item in storageItems)
            {
                if (item.IsOfType(StorageItemTypes.Folder))
                {
                    // Recursively get items from subfolders.
                    await GetFilesInFolderAsync((StorageFolder)item);
                }
                else if (item.IsOfType(StorageItemTypes.File))
                {
                    StorageFile file = (StorageFile)item;
                    Note note = new(fileService)
                    {
                        Filename = file.Name,
                        Text = await fileService.GetTextFromFileAsync(file),
                        Date = file.DateCreated.DateTime
                    };
                    Notes.Add(note);
                }
            }
        }
    }
}
