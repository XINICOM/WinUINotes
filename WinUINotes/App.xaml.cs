using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.Extensions.DependencyInjection;
using WinUINotes.Bus.ViewModels;
using WinUINotes.Bus.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUINotes
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        //private Window? _window;

        ///// <summary>
        ///// Initializes the singleton application object.  This is the first line of authored code
        ///// executed, and as such is the logical equivalent of main() or WinMain().
        ///// </summary>
        //public App()
        //{
        //    InitializeComponent();
        //}

        ///// <summary>
        ///// Invoked when the application is launched.
        ///// </summary>
        ///// <param name="args">Details about the launch request and process.</param>
        //protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        //{
        //    _window = new MainWindow();
        //    _window.Activate();
        //}

        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            Services = ConfigureServices();
            this.InitializeComponent();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Services
            services.AddSingleton<IFileService>(x =>
                ActivatorUtilities.CreateInstance<WindowsFileService>(x ,
                                Windows.Storage.ApplicationData.Current.LocalFolder)
            );

            // ViewModels
            services.AddTransient<AllNotesViewModel>();
            services.AddTransient<NoteViewModel>();

            return services.BuildServiceProvider();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        public IServiceProvider Services { get; }

        private Window? m_window;

        public new static App Current => (App)Application.Current;
    }
}
