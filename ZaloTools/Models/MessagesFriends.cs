namespace ZaloTools.Models;

public class MessagesFriends : BindableBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private string _name;

    public string Name
    {
        get { return _name; }
        set { SetProperty(ref _name, value); }
    }

    public virtual ObservableCollection<Friend> Friends { get; set; }
    public virtual ObservableCollection<ContentMessage> Messages { get; set; }
    public virtual SettingMessage SettingMessage { get; set; }
    public Guid AccountZaloId { get; set; }
}