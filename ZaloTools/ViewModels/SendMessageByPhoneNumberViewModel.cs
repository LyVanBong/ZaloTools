using ImTools;

namespace ZaloTools.ViewModels
{
    public class SendMessageByPhoneNumberViewModel : RegionViewModelBase
    {
        private readonly IAccountZaloRepository _accountZaloRepository;
        private ObservableCollection<AccountZalo> _accountZalos = new ObservableCollection<AccountZalo>();
        private MessagesFriend _messagesFriend = new MessagesFriend();
        private ObservableCollection<Friend> _friends = new ObservableCollection<Friend>();
        private SettingMessage _settingMessage = new SettingMessage();
        private ContentMessage _contentMessage = new ContentMessage();
        private ObservableCollection<ContentMessage> _contentMessages = new ObservableCollection<ContentMessage>();
        private readonly IMessagesFriendsRepository _messagesFriendsRepository;
        private ObservableCollection<MessagesFriend> _messagesFriends;
        public ObservableCollection<MessagesFriend> MessagesFriends
        {
            get { return _messagesFriends; }
            set { SetProperty(ref _messagesFriends, value); }
        }
        public ObservableCollection<ContentMessage> ContentMessages
        {
            get { return _contentMessages; }
            set { SetProperty(ref _contentMessages, value); }
        }
        public ContentMessage ContentMessage
        {
            get { return _contentMessage; }
            set { SetProperty(ref _contentMessage, value); }
        }
        public SettingMessage SettingMessage
        {
            get { return _settingMessage; }
            set { SetProperty(ref _settingMessage, value); }
        }

        public AccountZalo AccountZalo { get; set; } = new AccountZalo();
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
        public ObservableCollection<AccountZalo> AccountZalos
        {
            get => _accountZalos;
            set => SetProperty(ref _accountZalos, value);
        }

        public ICommand DeleteNumberPhoneCommand { get; private set; }
        public ICommand AddNumberPhoneCommand { get; private set; }
        public ICommand UpdateAccountZaloCommand { get; private set; }
        public ICommand AddContentMessageCommand { get; private set; }
        public ICommand CreateMessageToFriendCommand { get; private set; }
        public SendMessageByPhoneNumberViewModel(IRegionManager regionManager, IAccountZaloRepository accountZaloRepository, IMessagesFriendsRepository messagesFriendsRepository) : base(regionManager)
        {
            _accountZaloRepository = accountZaloRepository;
            _messagesFriendsRepository = messagesFriendsRepository;
            AddNumberPhoneCommand = new DelegateCommand(ExecuteAddNumberPhoneCommand);
            DeleteNumberPhoneCommand = new DelegateCommand<Friend>(ExecuteDeleteNumberPhoneCommand);
            UpdateAccountZaloCommand = new DelegateCommand(ExecuteUpdateAccountZaloCommand);
            AddContentMessageCommand = new DelegateCommand(ExecuteAddContentMessageCommand);
            CreateMessageToFriendCommand = new DelegateCommand(ExecuteCreateMessageToFriendCommand);
        }

        private void ExecuteCreateMessageToFriendCommand()
        {
            if (AccountZalo is not null && Friends.Any() && ContentMessages.Any())
            {

                MessageBox.Show("Thêm chiến dịch thành công");
                MessagesFriend.Friends = Friends;
                MessagesFriend.Messages = ContentMessages;
                MessagesFriend.SettingMessage = SettingMessage;
                MessagesFriend.NumberFriends = Friends.Count;
                MessagesFriend.MessageType = 1;
                MessagesFriend.AccountZalo = AccountZalo.Id;
                _messagesFriendsRepository.Add(MessagesFriend);
                MessagesFriends = new ObservableCollection<MessagesFriend>(_messagesFriendsRepository.GetAll(1));
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

        private void ExecuteAddContentMessageCommand()
        {
            if (ContentMessage.Content is not null)
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
        private void ExecuteDeleteNumberPhoneCommand(Friend obj)
        {
            if (obj is not null)
            {
                Friends.Remove(obj);
            }
            else
            {
                var lsDel = Friends.Where(x => x.IsSelected)?.ToList();
                if (lsDel is not null && lsDel.Any())
                {
                    lsDel.ForEach(x =>
                    {
                        Friends.Remove(x);
                    });
                }
            }
            SettingMessage.Limit = Friends.Count;
        }

        private void ExecuteAddNumberPhoneCommand()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Chọn danh sách số điện thoại";
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                {
                    var data = File.ReadAllText(openFileDialog.FileName);
                    if (data is not null)
                    {
                        var lsPhone = data.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                        lsPhone = lsPhone?.Distinct()?.ToArray();
                        if (lsPhone is not null && lsPhone.Any())
                        {
                            lsPhone.ForEach(x =>
                            {
                                Friends.Add(new Friend()
                                {
                                    Name = x,
                                    Status = "New",
                                });
                            });
                            SettingMessage.Limit = Friends.Count;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi vui lòng thử lại: " + e.Message);
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            AccountZalos = new ObservableCollection<AccountZalo>(_accountZaloRepository.GetAllZalo());
            MessagesFriends = new ObservableCollection<MessagesFriend>(_messagesFriendsRepository.GetAll(1));
            MessagesFriend.Name = "Gửi tin nhắn bằng số điện thoại: " + DateTime.Now.ToString("g");
        }
    }
}