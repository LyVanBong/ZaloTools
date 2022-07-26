namespace ZaloTools.Models;

public class SettingMessage : BindableBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private int _timeDelay = 10;
    private bool _isAddFriend;
    public bool IsAddFriend
    {
        get { return _isAddFriend; }
        set { SetProperty(ref _isAddFriend, value); }
    }
    public int TimeDelay
    {
        get { return _timeDelay; }
        set { SetProperty(ref _timeDelay, value); }
    }

    private int _numberOfDelayedMessages = 100;

    public int NumberOfDelayedMessages
    {
        get { return _numberOfDelayedMessages; }
        set { SetProperty(ref _numberOfDelayedMessages, value); }
    }

    private int _delayAfterSendingMessage = 120;

    public int DelayAfterSendingMessage
    {
        get { return _delayAfterSendingMessage; }
        set { SetProperty(ref _delayAfterSendingMessage, value); }
    }

    private int _limit;

    public int Limit
    {
        get { return _limit; }
        set { SetProperty(ref _limit, value); }
    }

    public Guid MessagesFriendsId { get; set; }

    [ForeignKey(nameof(MessagesFriendsId))]
    public virtual MessagesFriend MessagesFriend { get; set; }
}