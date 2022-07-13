namespace ZaloTools.Services;

public interface IChromeService
{
    /// <summary>
    /// Chụp ảnh 1 vùng nào đấy trên 1 trang web
    /// </summary>
    /// <param name="chromeDriver"></param>
    /// <param name="webElement"></param>
    /// <returns></returns>
    byte[] TakesScreenshot(ChromeDriver chromeDriver, IWebElement webElement);
    /// <summary>
    /// Đăng nhập vào zalo
    /// </summary>
    /// <param name="chromeDriver"></param>
    /// <returns></returns>
    Task<bool> CheckLoginZaloAsync(ChromeDriver chromeDriver);
    /// <summary>
    /// Mở chrome driver
    /// </summary>
    /// <param name="pathChromeProfile"></param>
    /// <param name="hideChrome">false nếu muốn hiển thị chrome</param>
    /// <returns></returns>
    Task<ChromeDriver> GetChromeDriverAsync(string pathChromeProfile, bool hideChrome = true);
}