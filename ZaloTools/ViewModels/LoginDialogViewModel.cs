namespace ZaloTools.ViewModels
{
    public class LoginDialogViewModel : BindableBase, IDialogAware
    {
        private IChromeService _chromeService;
        private ImageSource _qrcodeLogin;
        private ChromeDriver _chromeDriver;
        private string _pathChromeProfileDefault = Directory.GetCurrentDirectory() + "\\ChromeProfile";
        private string _lgoinZalo = "https://chat.zalo.me/";

        private int _login;
        private string _contentLogin = "Quét mã QR bằng Zalo để đăng nhập";
        private IAccountZaloRepository _accountZaloRepository;

        public int Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        public string ContentLogin
        {
            get { return _contentLogin; }
            set { SetProperty(ref _contentLogin, value); }
        }

        public AccountZalo AccountZalo { get; set; } = new AccountZalo();

        public string Title { get; } = "Đăng nhập tài khoản zalo";

        public event Action<IDialogResult> RequestClose;

        public ImageSource QrcodeLogin
        {
            get => _qrcodeLogin;
            set => SetProperty(ref _qrcodeLogin, value);
        }

        private DelegateCommand _loginCommand;

        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommand));

        public LoginDialogViewModel(IChromeService chromeService, DatabaseLocalContext databaseLocalContext, IAccountZaloRepository accountZaloRepository)
        {
            _chromeService = chromeService;
            _accountZaloRepository = accountZaloRepository;
        }

        private async void ExecuteLoginCommand()
        {
            try
            {
                if (AccountZalo.CheckAccountZalo())
                {
                    if (Login == 1)
                    {
                        Login = 0;
                        ContentLogin = "Quét mã QR bằng Zalo để đăng nhập";
                        await CheckLogin(_chromeDriver);
                        return;
                    }
                    ContentLogin = "Đang tải mã QR vui lòng đợi";
                    AccountZalo.PathProfileChrome = _pathChromeProfileDefault + "\\" + Guid.NewGuid();
                    ZipFile.ExtractToDirectory(_pathChromeProfileDefault + "\\ChromeProfileDefault.zip",
                        AccountZalo.PathProfileChrome);
                    if (Directory.Exists(AccountZalo.PathProfileChrome))
                    {
                        _chromeDriver = await _chromeService.GetChromeDriverAsync(AccountZalo.PathProfileChrome);
                        if (_chromeDriver != null)
                        {
                            _chromeDriver.Navigate().GoToUrl(_lgoinZalo);
                            await Task.Delay(TimeSpan.FromSeconds(5));

                            var shots = _chromeService.TakesScreenshot(_chromeDriver, _chromeDriver.FindElement(By.ClassName("in-tableCell")));
                            if (shots != null)
                            {
                                QrcodeLogin = ConvertByteArrayToImageSource(shots);
                                if (QrcodeLogin != null)
                                {
                                    Login = 1;
                                    ContentLogin = "Tiếp tục";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Lỗi vui lòng thử lại");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Lỗi vui lòng thử lại");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lỗi vui lòng thử lại");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi vui lòng thử lại");
                _chromeDriver?.Close();
                RequestClose(null);
            }
        }

        private ImageSource ConvertByteArrayToImageSource(byte[] shots)
        {
            BitmapImage bitmap = new BitmapImage();
            MemoryStream ms = new MemoryStream(shots);
            bitmap.BeginInit();
            bitmap.StreamSource = ms;
            bitmap.EndInit();
            return bitmap as ImageSource;
        }

        private async Task CheckLogin(ChromeDriver chromeDriver)
        {
            if (chromeDriver != null)
            {
                var login = await _chromeService.CheckLoginZaloAsync(chromeDriver);
                if (login)
                {
                    AccountZalo.Status = "Đã đăng nhập";
                    _accountZaloRepository.AddZalo(AccountZalo);
                    MessageBox.Show("Đăng nhập tài khoản zalo thành công");
                    _chromeDriver?.Close();
                    RequestClose(new DialogResult(ButtonResult.OK));
                }
                else
                {
                    MessageBox.Show("Lỗi vui lòng thử lại");
                    _chromeDriver?.Close();
                    RequestClose(null);
                }
            }
            else
            {
                MessageBox.Show("Lỗi vui lòng thử lại");
                _chromeDriver?.Close();
                RequestClose(null);
            }
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            //if (parameters != null)
            //{
            //    AccountZalo = parameters.GetValue<AccountZalo>(nameof(AccountZalo));
            //}
        }
    }
}