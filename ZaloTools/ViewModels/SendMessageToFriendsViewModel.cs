namespace ZaloTools.ViewModels
{
    public class SendMessageToFriendsViewModel : RegionViewModelBase
    {
        private readonly IMessagesFriendsRepository _messagesFriendsRepository;
        private readonly IAccountZaloRepository _accountZaloRepository;
        private ObservableCollection<AccountZalo> _accountZalos = new ObservableCollection<AccountZalo>();
        public ICommand UpdateAccountZaloCommand { set; get; }
        public ObservableCollection<AccountZalo> AccountZalos
        {
            get { return _accountZalos; }
            set { SetProperty(ref _accountZalos, value); }
        }
        public SendMessageToFriendsViewModel(IRegionManager regionManager, IMessagesFriendsRepository messagesFriendsRepository, IAccountZaloRepository accountZaloRepository) : base(regionManager)
        {
            _messagesFriendsRepository = messagesFriendsRepository;
            _accountZaloRepository = accountZaloRepository;
            UpdateAccountZaloCommand = new DelegateCommand(ExcuteUpdateAccountZaloCommand);
        }

        private void ExcuteUpdateAccountZaloCommand()
        {
            var accountZalos = _accountZaloRepository.GetAllZalo();
            if (accountZalos != null && accountZalos.Any())
            {
                AccountZalos = new ObservableCollection<AccountZalo>(accountZalos);
                MessageBox.Show("Cập nhật thành công");
            }
            else
            {
                MessageBox.Show("Chưa có tài khoản zalo nào, vui lòng đăng nhập zalo !");
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            var accountZalos = _accountZaloRepository.GetAllZalo();
            if (accountZalos != null && accountZalos.Any())
            {
                AccountZalos.AddRange(accountZalos);
            }
        }
    }
}