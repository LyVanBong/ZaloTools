namespace ZaloTools.Services;

public interface ICacheMemoryService
{
    /// <summary>
    /// Check xem co dialog nao đang mở không
    /// </summary>
    bool IsOpenDialog { get; set; }
}