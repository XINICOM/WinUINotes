using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WinUINotes.Bus.Services;

namespace WinUINotes.Bus.Models
{
    public class Note
    {
        //private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        private IFileService fileService;
        public string Filename { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;

        public Note(IFileService fileService)
        {
            Filename = "notes" + DateTime.Now.ToBinary().ToString() + ".tant";
            //Filename = "note"+DateTime.Now.ToBinary().ToString() +".txt";
            this.fileService = fileService;
        }

        public async Task SaveAsync()
        {
            //StorageFile noteFile = (StorageFile)await storageFolder.TryGetItemAsync(Filename);
            ////Console.WriteLine("successFloder");
            ////Text="successFloder";
            //if (noteFile is null)
            //{
            //    noteFile = await storageFolder.CreateFileAsync(Filename, CreationCollisionOption.ReplaceExisting);
            //}
            //await FileIO.WriteTextAsync(noteFile, Text);
            await fileService.CreateOrUpdateFileAsync(Filename , Text);
        }

        public async Task DeleteAsync()
        {
            //StorageFile noteFile = (StorageFile)await storageFolder.TryGetItemAsync(Filename);
            //if (noteFile is not null)
            //{
            //    await noteFile.DeleteAsync();
            //}
            await fileService.DeleteFileAsync(Filename);
        }

        public bool NoteFileExists()
        {
            return fileService.FileExists(Filename);
        }
    }
}
