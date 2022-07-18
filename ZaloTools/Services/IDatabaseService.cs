namespace ZaloTools.Services;

public interface IDatabaseService
{
    /// <summary>
    /// Thêm tài khoản zalo
    /// </summary>
    /// <param name="accountZalo"></param>
    /// <returns></returns>
    bool AddZalo(AccountZalo accountZalo);

    /// <summary>
    /// Cập nhật tài khoản zalo
    /// </summary>
    /// <param name="accountZalo"></param>
    /// <returns></returns>
    bool UpdateZalo(AccountZalo accountZalo);

    /// <summary>
    /// Xóa tài khoản zalo
    /// </summary>
    /// <param name="accountZalo"></param>
    /// <returns></returns>
    bool DeleteZalo(AccountZalo accountZalo);

    /// <summary>
    /// Lây thông tin 1 tài khoản zalo
    /// </summary>
    /// <param name="accountZalo"></param>
    /// <returns></returns>
    AccountZalo GetZalo(AccountZalo accountZalo);

    /// <summary>
    /// Lấy thông tin tất cả tài khoản zalo đã lưu
    /// </summary>
    /// <returns></returns>
    List<AccountZalo> GetAllZalo();
}