namespace ZaloTools.Models;

public class MessagesFriends : BindableBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private string _name;
    private bool _isSelected;
    private string _status = "New";

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

    public virtual ObservableCollection<Friend> Friends { get; set; }
    public virtual ObservableCollection<ContentMessage> Messages { get; set; }
    public virtual SettingMessage SettingMessage { get; set; }
    public Guid AccountZaloId { get; set; }
}