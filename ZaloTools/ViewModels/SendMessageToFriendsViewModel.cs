namespace ZaloTools.ViewModels
{
    public class SendMessageToFriendsViewModel : RegionViewModelBase
    {
        private readonly IMessagesFriendsRepository _messagesFriendsRepository;
        private readonly IAccountZaloRepository _accountZaloRepository;
        private ObservableCollection<AccountZalo> _accountZalos = new ObservableCollection<AccountZalo>();
        private MessagesFriends _messagesFriends = new MessagesFriends();
        private readonly IChromeService _chromeService;
        private string _lgoinZalo = "https://chat.zalo.me/";
        private ObservableCollection<Friend> _friends = new ObservableCollection<Friend>();
        public ObservableCollection<Friend> Friends
        {
            get { return _friends; }
            set { SetProperty(ref _friends, value); }
        }
        public MessagesFriends MessagesFriends
        {
            get { return _messagesFriends; }
            set { SetProperty(ref _messagesFriends, value); }
        }
        public ICommand UpdateAccountZaloCommand { set; get; }
        public ObservableCollection<AccountZalo> AccountZalos
        {
            get { return _accountZalos; }
            set { SetProperty(ref _accountZalos, value); }
        }

        public ICommand UpdateFriendZaloCommand { get; private set; }
        public ICommand DeleteFriendZaloCommand { get; private set; }
        public SendMessageToFriendsViewModel(IRegionManager regionManager, IMessagesFriendsRepository messagesFriendsRepository, IAccountZaloRepository accountZaloRepository, IChromeService chromeService) : base(regionManager)
        {
            _messagesFriendsRepository = messagesFriendsRepository;
            _accountZaloRepository = accountZaloRepository;
            _chromeService = chromeService;
            UpdateAccountZaloCommand = new DelegateCommand(ExcuteUpdateAccountZaloCommand);
            UpdateFriendZaloCommand = new DelegateCommand<AccountZalo>(res => _ = ExcuteUpdateFriendZaloCommand(res));
            DeleteFriendZaloCommand = new DelegateCommand<Friend>(ExcuteDeleteFriendZaloCommand);
        }

        private void ExcuteDeleteFriendZaloCommand(Friend friend)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                if (friend is not null)
                {
                    Friends.Remove(friend);
                }
                else
                {
                    var range = Friends.Where(x => x.IsSelected)?.ToList();
                    if (range is not null)
                    {
                        range.ForEach(frd =>
                        {
                            Friends.Remove(frd);
                        });
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi phát sinh vui long thử lại: " + e.Message);
            }
            IsBusy = false;
        }

        private async Task ExcuteUpdateFriendZaloCommand(AccountZalo obj)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                if (obj == null)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản !");
                }
                else
                {
                    var driver = await _chromeService.GetChromeDriverAsync(obj.PathProfileChrome, false);
                    driver.Navigate().GoToUrl(_lgoinZalo);
                    await Task.Delay(3000);
                    var element =
                        driver.FindElement(By.XPath("/html/body/div/div/div[2]/nav/div[1]/div[1]/div[2]/div[2]/i"));
                    element.Click();
                    await Task.Delay(2000);
                    int num = 0;
                    while (true)
                    {
                        var source1 = driver.PageSource;
                        Regex regex = new Regex(@"<div class=""conv-item-title flx flx-al-c rel w100"" style=""margin-top: 0px;""><div class=""conv-item-title__name truncate""><span class=""truncate"">(.*?)</span></div>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                        MatchCollection matchCollection = regex.Matches(source1);
                        foreach (Match match in matchCollection)
                        {
                            var value = HttpUtility.HtmlDecode(match.Groups[1].Value);
                            if (value != "Zalo" && !value.StartsWith("Cloud") && !value.Contains("KEY_NAME_SEND2ME"))
                            {
                                if (!Friends.Any(x => x.Name == value))
                                {
                                    Friends.Add(new Friend { Name = value });
                                }
                            }
                        }

                        var a = driver.FindElements(By.ClassName("conv-item-title__more"));
                        new Actions(driver).MoveToElement(a.Last()).Perform();
                        var source2 = driver.PageSource;
                        num++;
                        if (num == 100)
                        {
                            if (source2 == source1)
                            {
                                break;
                            }
                            else
                            {
                                num = 0;
                            }
                        }
                    }
                    driver.Close();
                    MessageBox.Show("Thành công");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi phát sinh vui lòng thử lại: " + e.Message);
            }
            IsBusy = false;
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

            MessagesFriends.Name = "Gửi tin nhắn cho bạn bè " + DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}