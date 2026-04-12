using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WinUINotes.Bus.Models
{
    public class Note
    {
        private StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        public string Filename { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;

        public Note()
        {
            Filename = "notes" + DateTime.Now.ToBinary().ToString() + ".tant";
        }

        public async Task SaveAsync()
        {
            StorageFile noteFile = (StorageFile)await storageFolder.TryGetItemAsync(Filename);
            //Console.WriteLine("successFloder");
            //Text="successFloder";
            if (noteFile is null)
            {
                noteFile = await storageFolder.CreateFileAsync(Filename, CreationCollisionOption.ReplaceExisting);
            }
            await FileIO.WriteTextAsync(noteFile, Text);
        }

        public async Task DeleteAsync()
        {
            StorageFile noteFile = (StorageFile)await storageFolder.TryGetItemAsync(Filename);
            if (noteFile is not null)
            {
                await noteFile.DeleteAsync();
            }
        }
    }
}
