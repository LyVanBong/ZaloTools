namespace ZaloTools.Services;

public interface IMessagesFriendsRepository
{
    /// <summary>
    /// Kiểm tra sư tồn tại của chiến dịch
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    bool Exist(Guid Id);

    /// <summary>
    /// Xóa chiến dịch
    /// </summary>
    /// <param name="messagesFriend"></param>
    /// <returns></returns>
    bool Delete(MessagesFriend messagesFriend);

    /// <summary>
    /// Thêm chiến dịch
    /// </summary>
    /// <param name="messagesFriend"></param>
    /// <returns></returns>
    bool Add(MessagesFriend messagesFriend);

    /// <summary>
    /// Cập nhật chiến dịch
    /// </summary>
    /// <param name="messagesFriend"></param>
    /// <returns></returns>
    bool Update(MessagesFriend messagesFriend);

    /// <summary>
    /// Tìm 1 chiến dịch cụ thể
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    MessagesFriend Get(Guid Id);

    /// <summary>
    /// Lấy toàn bộ chiến dịch
    /// </summary>
    /// <param name="messageType"></param>
    /// <returns></returns>
    List<MessagesFriend> GetAll(int messageType);
}