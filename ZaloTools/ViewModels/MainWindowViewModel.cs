using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using System.Windows.Input;
using ZaloTools.Models;
using ZaloTools.Views;

namespace ZaloTools.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Zalo Marketing";
        private IRegionManager _regionManager;
        private MenuApp _menuApp = new MenuApp();
        private string _regionNames = "ContentRegion";

        public MenuApp MenuApp
        {
            get => _menuApp;
            set => SetProperty(ref _menuApp, value);
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ICommand DashboardCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            DashboardCommand = new DelegateCommand<string>(ExcutedDashboardCommand);
        }

        private void ExcutedDashboardCommand(string key)
        {
            switch (key)
            {
                case "0":
                    MenuApp.SelectButton(1);
                    CreateDefaultView();
                    break;

                case "1":
                    MenuApp.SelectButton(2);
                    _regionManager.RequestNavigate(_regionNames, "/" + nameof(AcountView));
                    break;

                case "2":
                    if (MenuApp.Message)
                        MenuApp.SelectButtonMessage(-1);
                    else
                        MenuApp.SelectButtonMessage();
                    break;

                case "3":
                    MenuApp.SelectButtonMessage(1);
                    break;

                case "4":
                    MenuApp.SelectButtonMessage(2);
                    break;

                case "5":
                    MenuApp.SelectButtonMessage(3);
                    break;

                case "6":
                    if (MenuApp.AddFriend) MenuApp.SelectButtonAddFriend(-1);
                    else
                        MenuApp.SelectButtonAddFriend();
                    break;

                case "7":
                    MenuApp.SelectButtonAddFriend(1);
                    break;

                case "8":
                    MenuApp.SelectButtonAddFriend(2);
                    break;

                case "9":
                    MenuApp.SelectButtonAddFriend(3);
                    break;

                case "10":
                    MenuApp.SelectButton(3);
                    _regionManager.RequestNavigate(_regionNames, "/" + nameof(ScanNumberPhoneView));
                    break;

                case "11":
                    MenuApp.SelectButton(4);
                    _regionManager.RequestNavigate(_regionNames, "/" + nameof(AboutView));
                    break;

                default:
                    MessageBox.Show("Tính năng này đang trong qua trình phát triển");
                    break;
            }
        }

        public void CreateDefaultView()
        {
            _regionManager.RequestNavigate(_regionNames, "/" + nameof(DashboardView));
        }
    }
}