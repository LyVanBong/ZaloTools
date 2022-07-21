namespace ZaloTools.ViewModels
{
    public class AcountViewModel : RegionViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICacheMemoryService _cacheMemoryService;
        private ObservableCollection<AccountZalo> _accountZalos = new ObservableCollection<AccountZalo>();
        private readonly IAccountZaloRepository _accountZaloRepository;
        private readonly IChromeService _chromeService;
        private string _lgoinZalo = "https://chat.zalo.me/";

        public ICommand LoginCommand { get; private set; }

        public ObservableCollection<AccountZalo> AccountZalos
        {
            get { return _accountZalos; }
            set { _accountZalos = value; }
        }

        private DelegateCommand<AccountZalo> _deleteAccountZaloCommand;

        public DelegateCommand<AccountZalo> DeleteAccountZaloCommand =>
            _deleteAccountZaloCommand ?? (_deleteAccountZaloCommand = new DelegateCommand<AccountZalo>(ExecuteDeleteAccountZaloCommand));

        public ICommand ReLoginAccountZaloCommand { get; private set; }
        public ICommand CheckIboxZaloCommand { get; private set; }

        public AcountViewModel(IDialogService dialogService, ICacheMemoryService cacheMemoryService, IAccountZaloRepository accountZaloRepository, IRegionManager regionManager, IChromeService chromeService) : base(regionManager)
        {
            _dialogService = dialogService;
            _cacheMemoryService = cacheMemoryService;
            _accountZaloRepository = accountZaloRepository;
            _chromeService = chromeService;
            LoginCommand = new DelegateCommand<string>(ExcuteLoginCommand);
            ReLoginAccountZaloCommand = new DelegateCommand<AccountZalo>(ExecuteReLoginAccountZaloCommand);
            CheckIboxZaloCommand = new DelegateCommand<AccountZalo>(ExcuteCheckIboxZaloCommand);
        }

        private async void ExcuteCheckIboxZaloCommand(AccountZalo obj)
        {
            var drive = await _chromeService.GetChromeDriverAsync(obj.PathProfileChrome, false);
            drive.Navigate().GoToUrl(_lgoinZalo);
        }

        private void ExecuteReLoginAccountZaloCommand(AccountZalo obj)
        {
            var para = new DialogParameters();
            para.Add(nameof(AccountZalos), obj);
            _dialogService.Show(nameof(LoginDialog), para, result =>
            {
                if (result == null) return;
                if (result.Result == ButtonResult.OK)
                {
                    GetAccountZalo();
                }
            });
        }

        private void ExecuteDeleteAccountZaloCommand(AccountZalo accountZalo)
        {
            if (Directory.Exists(accountZalo.PathProfileChrome))
            {
                Directory.Delete(accountZalo.PathProfileChrome, true);
                if (_accountZaloRepository.DeleteZalo(accountZalo))
                {
                    AccountZalos.Remove(accountZalo);
                }
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetAccountZalo();
        }

        private Task GetAccountZalo()
        {
            var data = _accountZaloRepository.GetAllZalo();
            if (data != null && data.Any() && data[0].CheckAccountZalo())
            {
                AccountZalos.Clear();
                AccountZalos.AddRange(data);
            }
            return Task.CompletedTask;
        }

        private void ExcuteLoginCommand(string obj)
        {
            _dialogService.Show(nameof(LoginDialog), result =>
            {
                if (result == null) return;
                if (result.Result == ButtonResult.OK)
                {
                    GetAccountZalo();
                }
            });
            _cacheMemoryService.IsOpenDialog = true;
        }
    }
}