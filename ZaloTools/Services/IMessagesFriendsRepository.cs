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
    /// <param name="messagesFriends"></param>
    /// <returns></returns>
    bool Delete(MessagesFriends messagesFriends);

    /// <summary>
    /// Thêm chiến dịch
    /// </summary>
    /// <param name="messagesFriends"></param>
    /// <returns></returns>
    bool Add(MessagesFriends messagesFriends);

    /// <summary>
    /// Cập nhật chiến dịch
    /// </summary>
    /// <param name="messagesFriends"></param>
    /// <returns></returns>
    bool Update(MessagesFriends messagesFriends);

    /// <summary>
    /// Tìm 1 chiến dịch cụ thể
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    MessagesFriends Get(Guid Id);

    /// <summary>
    /// Lấy toàn bộ chiến dịch
    /// </summary>
    /// <returns></returns>
    List<MessagesFriends> GetAll();
}