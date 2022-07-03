using System;
using System.Windows.Input;
using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ZaloTools.Views;

namespace ZaloTools.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Zalo Marketing";
        private IRegionManager _regionManager;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        public ICommand DashboardCommand { get; private set; }
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            DashboardCommand = new DelegateCommand(ExcutedDashboardCommand);
            _regionManager.RequestNavigate("ContentRegion", nameof(DashboardView));
        }

        private void ExcutedDashboardCommand()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(DashboardView));
        }
    }
}
