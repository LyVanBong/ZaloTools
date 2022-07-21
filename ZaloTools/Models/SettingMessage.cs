namespace ZaloTools.Models;

public class SettingMessage : BindableBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private int _timeDelay;

    public int TimeDelay
    {
        get { return _timeDelay; }
        set { SetProperty(ref _timeDelay, value); }
    }

    private int _numberOfDelayedMessages;

    public int NumberOfDelayedMessages
    {
        get { return _numberOfDelayedMessages; }
        set { SetProperty(ref _numberOfDelayedMessages, value); }
    }

    private int _delayAfterSendingMessage;

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
    public virtual MessagesFriends MessagesFriends { get; set; }
}