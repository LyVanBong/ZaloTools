namespace ZaloTools.Services;

public interface IBackgroundWorkerService
{
    /// <summary>
    /// Tắt IBackgroundWorkerService
    /// </summary>
    /// <returns></returns>
    Task StopAsync();
    /// <summary>
    /// Bật IBackgroundWorkerService
    /// </summary>
    /// <returns></returns>
    Task StartAsync();
}