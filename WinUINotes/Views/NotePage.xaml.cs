using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using WinUINotes.Bus.Messages;
using WinUINotes.Bus.Models;
using WinUINotes.Bus.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUINotes.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotePage : Page
    {
        
        private NoteViewModel? noteVM;

        public NotePage()
        {
            this.InitializeComponent();
        }

        public void RegisterForDeleteMessages()
        {
            WeakReferenceMessenger.Default.Register<NoteDeletedMessage>(this , (o, m) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            });
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            noteVM = App.Current.Services.GetService<NoteViewModel>();
            RegisterForDeleteMessages();
            if (e.Parameter is Note note && noteVM is not null)
            {
                noteVM.InitializeForExistingNote(note);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            WeakReferenceMessenger.Default.Unregister<NoteDeletedMessage>(this);
            base.OnNavigatedFrom(e);
        }
    }
}
