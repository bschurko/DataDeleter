using DataDeleter.Services;
using DataDeleter.Utils;
using DataDeleter.ViewModel;
using SimpleInjector;
using System.Configuration;
using System.Data;
using System.Windows;

namespace DataDeleter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            // 2. Configure container settings (optional, but recommended)
            IoC.Container.Options.EnableAutoVerification = true;

            // 3. Register your services, view models, and windows
            RegisterServices(IoC.Container);

            // 4. Verify the container setup
            IoC.Container.Verify();

            // 5. Resolve and show the main window
            var mainWindow = IoC.Container.GetInstance<MainWindow>();
            mainWindow.Show();
        }

        private void RegisterServices(SimpleInjector.Container container)
        {
            // Register services interface-to-implementation
            container.Register<IFolderDialog, FolderDialog>(Lifestyle.Singleton);

            // Register ViewModels (Transient creates a new instance each time requested)
            container.Register<MainViewModel>(Lifestyle.Singleton);

            // Register Windows
            container.Register<MainWindow>(Lifestyle.Singleton);
        }


    }

}
