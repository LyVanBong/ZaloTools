namespace ZaloTools.Models;

public class ContentMessage : BindableBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private string _content;

    public string Content
    {
        get { return _content; }
        set { SetProperty(ref _content, value); }
    }

    private byte[] _images;

    public byte[] Images
    {
        get { return _images; }
        set { SetProperty(ref _images, value); }
    }

    private bool _status;

    public bool Status
    {
        get { return _status; }
        set { SetProperty(ref _status, value); }
    }

    public virtual MessagesFriends MessagesFriends { get; set; }
}