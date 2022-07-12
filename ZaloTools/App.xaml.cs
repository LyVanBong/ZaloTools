using Prism.Ioc;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using ZaloTools.Database;
using ZaloTools.Services;
using ZaloTools.ViewModels;
using ZaloTools.Views;

namespace ZaloTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Đăng ký database local
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseLocalContext>();
            containerRegistry.RegisterInstance(optionsBuilder.Options);

            containerRegistry.RegisterScoped<IChromeService, ChromeService>();
            containerRegistry.RegisterScoped<IDatabaseService, DatabaseService>();

            containerRegistry.RegisterSingleton<ICacheMemoryService, CacheMemoryService>();

            containerRegistry.RegisterDialog<LoginDialog, LoginDialogViewModel>();

            containerRegistry.RegisterForNavigation<DashboardView, DashboardViewModel>();
            containerRegistry.RegisterForNavigation<AcountView, AcountViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();
            containerRegistry.RegisterForNavigation<ScanNumberPhoneView, ScanNumberPhoneViewModel>();
        }
    }
}