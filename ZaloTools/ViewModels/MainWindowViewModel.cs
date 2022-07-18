namespace ZaloTools.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Zalo Marketing - Phần mềm hỗ trợ gửi tin nhắn zalo marketing online";
        private IRegionManager _regionManager;
        private MenuApp _menuApp = new MenuApp();
        private string _regionNames = "ContentRegion";
        private readonly ICacheMemoryService _cacheMemoryService;
        private string _pathChromeProfileDefault = Directory.GetCurrentDirectory() + "\\ChromeProfile";

        private string _profileChromeDefault =
            "https://drive.google.com/u/2/uc?id=1pDd1N3VqO_f-UuC-73h8NOsWEjqhV5FS&export=download&confirm=t";

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

        public MainWindowViewModel(IRegionManager regionManager, ICacheMemoryService cacheMemoryService)
        {
            _regionManager = regionManager;
            _cacheMemoryService = cacheMemoryService;
            DashboardCommand = new DelegateCommand<string>(ExcutedDashboardCommand);
            _ = CreateDefaultData();
        }

        private async Task CreateDefaultData()
        {
            try
            {
                _cacheMemoryService.IsOpenDialog = true;
                if (!Directory.Exists(_pathChromeProfileDefault))
                {
                    Directory.CreateDirectory(_pathChromeProfileDefault);

                    await DownloadFile(_profileChromeDefault, _pathChromeProfileDefault + "\\ChromeProfileDefault.zip");
                }
                else
                {
                    if (File.Exists(_pathChromeProfileDefault + "\\ChromeProfileDefault.zip"))
                    {
                    }
                    else
                    {
                        await DownloadFile(_profileChromeDefault,
                            _pathChromeProfileDefault + "\\ChromeProfileDefault.zip");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            finally
            {
                _cacheMemoryService.IsOpenDialog = false;
            }
        }

        private async Task DownloadFile(string url, string pathFile)
        {
            var downloadOpt = new DownloadConfiguration()
            {
                ChunkCount = 8, // file parts to download, default value is 1
                OnTheFlyDownload = true, // caching in-memory or not? default values is true
                ParallelDownload = true // download parts of file as parallel or not. Default value is false
            };

            var downloader = new DownloadService(downloadOpt);

            await downloader.DownloadFileTaskAsync(url, pathFile);
        }

        private void ExcutedDashboardCommand(string key)
        {
            if (_cacheMemoryService.IsOpenDialog) return;
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
                    _regionManager.RequestNavigate(_regionNames, "/" + nameof(SendMessageToFriendsView));
                    break;

                case "4":
                    MenuApp.SelectButtonMessage(2);
                    _regionManager.RequestNavigate(_regionNames, "/" + nameof(SendMessageByPhoneNumberView));
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