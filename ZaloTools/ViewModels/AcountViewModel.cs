using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ZaloTools.Models;
using ZaloTools.Services;
using ZaloTools.Views;

namespace ZaloTools.ViewModels
{
    public class AcountViewModel : RegionViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICacheMemoryService _cacheMemoryService;
        private ObservableCollection<AccountZalo> _accountZalos = new ObservableCollection<AccountZalo>();
        private readonly IDatabaseService _databaseService;

        public ICommand LoginCommand { get; private set; }

        public ObservableCollection<AccountZalo> AccountZalos
        {
            get { return _accountZalos; }
            set { _accountZalos = value; }
        }

        public AcountViewModel(IDialogService dialogService, ICacheMemoryService cacheMemoryService, IDatabaseService databaseService, IRegionManager regionManager) : base(regionManager)
        {
            _dialogService = dialogService;
            _cacheMemoryService = cacheMemoryService;
            _databaseService = databaseService;
            LoginCommand = new DelegateCommand<string>(ExcuteLoginCommand);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            var data = _databaseService.GetAllZalo();
            if (data != null && data.Any() && data[0].CheckAccountZalo())
            {
                AccountZalos.Clear();
                AccountZalos.AddRange(data);
            }
        }

        private void ExcuteLoginCommand(string obj)
        {
            _dialogService.Show(nameof(LoginDialog), result =>
            {
                if (result == null) return;
                if (result.Result == ButtonResult.OK)
                {
                    var data = _databaseService.GetAllZalo();
                    if (data != null && data.Any() && data[0].CheckAccountZalo())
                    {
                        AccountZalos.Clear();
                        AccountZalos.AddRange(data);
                    }
                }
            });
            _cacheMemoryService.IsOpenDialog = true;
        }
    }
}