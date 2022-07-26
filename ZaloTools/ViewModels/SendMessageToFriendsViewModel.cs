namespace ZaloTools.ViewModels
{
    public class SendMessageToFriendsViewModel : RegionViewModelBase
    {
        private readonly IMessagesFriendsRepository _messagesFriendsRepository;
        private readonly IAccountZaloRepository _accountZaloRepository;
        private ObservableCollection<AccountZalo> _accountZalos = new ObservableCollection<AccountZalo>();
        private MessagesFriend _messagesFriend = new MessagesFriend();
        private readonly IChromeService _chromeService;
        private string _lgoinZalo = "https://chat.zalo.me/";
        private ObservableCollection<Friend> _friends = new ObservableCollection<Friend>();

        private ObservableCollection<ContentMessage> _contentMessages = new ObservableCollection<ContentMessage>();
        private ContentMessage _contentMessage = new ContentMessage();
        private SettingMessage _settingMessage = new SettingMessage();

        private ObservableCollection<MessagesFriend> _messagesFriends = new ObservableCollection<MessagesFriend>();
        public AccountZalo AccountZalo { get; set; } = new AccountZalo();
        public ObservableCollection<MessagesFriend> MessagesFriends
        {
            get { return _messagesFriends; }
            set { SetProperty(ref _messagesFriends, value); }
        }

        public SettingMessage SettingMessage
        {
            get { return _settingMessage; }
            set { SetProperty(ref _settingMessage, value); }
        }

        public ContentMessage ContentMessage
        {
            get { return _contentMessage; }
            set { SetProperty(ref _contentMessage, value); }
        }

        public ObservableCollection<ContentMessage> ContentMessages
        {
            get { return _contentMessages; }
            set { SetProperty(ref _contentMessages, value); }
        }

        public ObservableCollection<Friend> Friends
        {
            get { return _friends; }
            set { SetProperty(ref _friends, value); }
        }

        public MessagesFriend MessagesFriend
        {
            get { return _messagesFriend; }
            set { SetProperty(ref _messagesFriend, value); }
        }

        public ICommand UpdateAccountZaloCommand { set; get; }

        public ObservableCollection<AccountZalo> AccountZalos
        {
            get { return _accountZalos; }
            set { SetProperty(ref _accountZalos, value); }
        }

        public ICommand UpdateFriendZaloCommand { get; private set; }
        public ICommand DeleteFriendZaloCommand { get; private set; }
        public ICommand AddContentMessageCommand { get; private set; }
        public ICommand CreateMessageToFriendCommand { get; private set; }

        public SendMessageToFriendsViewModel(IRegionManager regionManager, IMessagesFriendsRepository messagesFriendsRepository, IAccountZaloRepository accountZaloRepository, IChromeService chromeService) : base(regionManager)
        {
            _messagesFriendsRepository = messagesFriendsRepository;
            _accountZaloRepository = accountZaloRepository;
            _chromeService = chromeService;
            UpdateAccountZaloCommand = new DelegateCommand(ExecuteUpdateAccountZaloCommand);
            UpdateFriendZaloCommand = new DelegateCommand<AccountZalo>(res => _ = ExcuteUpdateFriendZaloCommand(res));
            DeleteFriendZaloCommand = new DelegateCommand<Friend>(ExecuteDeleteFriendZaloCommand);
            AddContentMessageCommand = new DelegateCommand<string>(ExecuteAddContentMessageCommand);
            CreateMessageToFriendCommand = new DelegateCommand(ExecuteCreateMessageToFriendCommand);
        }

        private void ExecuteCreateMessageToFriendCommand()
        {
            if (Friends.Any() && ContentMessages.Any())
            {
                MessageBox.Show("Thêm chiến dịch thành công");
                MessagesFriend.Friends = Friends;
                MessagesFriend.Messages = ContentMessages;
                MessagesFriend.SettingMessage = SettingMessage;
                MessagesFriend.NumberFriends = Friends.Count;
                MessagesFriend.MessageType = 0;
                MessagesFriend.AccountZalo = AccountZalo.Id;
                _messagesFriendsRepository.Add(MessagesFriend);
                MessagesFriends = new ObservableCollection<MessagesFriend>(_messagesFriendsRepository.GetAll(0));
                Friends = new ObservableCollection<Friend>();
                ContentMessages = new ObservableCollection<ContentMessage>();
                SettingMessage = new SettingMessage();
                ContentMessage = new ContentMessage();
            }
            else
            {
                MessageBox.Show("Vui lòng thiết lập chiến dịch");
            }
        }

        private void ExecuteAddContentMessageCommand(string key)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                if (key == "0")
                {
                    OpenFileDialog res = new OpenFileDialog();
                    res.Title = "Chọn tệp tin muốn gửi";
                    if (res.ShowDialog() == true)
                    {
                        ContentMessage.PathImage = res.FileName;
                        ContentMessage.NameImage = res.SafeFileName;
                        ContentMessage.Images = File.ReadAllBytes(res.FileName);
                    }
                }
                else if (key == "1")
                {
                    if (ContentMessage.CheckContentMessage())
                    {
                        MessageBox.Show("Thêm tin nhắn thành công");
                        var content = ContentMessages.FirstOrDefault(x => x.Id == ContentMessage.Id);
                        if (content is not null)
                        {
                            content.Images = ContentMessage.Images;
                            content.PathImage = ContentMessage.PathImage;
                            content.Content = ContentMessage.Content;
                            content.NameImage = ContentMessage.NameImage;
                        }
                        else
                            ContentMessages.Add(ContentMessage);
                        ContentMessage = new ContentMessage();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng điền nội dung tin nhắn");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi phát sinh vui long thử lại: " + e.Message);
            }
            IsBusy = false;
        }

        private void ExecuteDeleteFriendZaloCommand(Friend friend)
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
                    var driver = await _chromeService.GetChromeDriverAsync(obj.PathProfileChrome);
                    driver.Navigate().GoToUrl(_lgoinZalo);
                    await Task.Delay(3000);

                    #region lấy danh sách bạn bè

                    var element =
                        driver.FindElement(By.XPath("/html/body/div/div/div[2]/nav/div[1]/div[1]/div[2]/div[2]/i"));
                    element.Click();
                    await Task.Delay(2000);
                    int num = 0;
                    while (true)
                    {
                        Debug.WriteLine("Scroll: " + num);
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

                    #endregion lấy danh sách bạn bè

                    SettingMessage.Limit = Friends.Count;
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

        private void ExecuteUpdateAccountZaloCommand()
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
            AccountZalos = new ObservableCollection<AccountZalo>(_accountZaloRepository.GetAllZalo());
            MessagesFriends = new ObservableCollection<MessagesFriend>(_messagesFriendsRepository.GetAll(0));
            MessagesFriend.Name = "Gửi tin nhắn cho bạn bè: " + DateTime.Now.ToString("g");
        }
    }
}