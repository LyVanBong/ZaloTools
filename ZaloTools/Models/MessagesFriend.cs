namespace ZaloTools.Models;

public class MessagesFriend : BindableBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private string _name;
    private bool _isSelected;
    private string _status = "New";
    private int _messageType;
    public int MessageType
    {
        get { return _messageType; }
        set { SetProperty(ref _messageType, value); }
    }
    public string Status
    {
        get { return _status; }
        set { SetProperty(ref _status, value); }
    }

    public bool IsSelected
    {
        get { return _isSelected; }
        set { SetProperty(ref _isSelected, value); }
    }

    public string Name
    {
        get { return _name; }
        set { SetProperty(ref _name, value); }
    }

    private int _numberFriends;

    public int NumberFriends
    {
        get { return _numberFriends; }
        set { SetProperty(ref _numberFriends, value); }
    }
    public DateTime CreateDate { get; } = DateTime.Now;
    public virtual ObservableCollection<Friend> Friends { get; set; }
    public virtual ObservableCollection<ContentMessage> Messages { get; set; }
    public virtual SettingMessage SettingMessage { get; set; }
    public Guid AccountZalo { get; set; }
}