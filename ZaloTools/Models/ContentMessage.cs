namespace ZaloTools.Models;

public class ContentMessage : BindableBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private string _content;
    private string _name = "Nội dung tin nhắn: " + DateTime.Now.ToString("g");
    private string _nameImage;

    public string NameImage
    {
        get { return _nameImage; }
        set { SetProperty(ref _nameImage, value); }
    }

    public string Name
    {
        get { return _name; }
        set { SetProperty(ref _name, value); }
    }

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

    private string _pathImage;

    public string PathImage
    {
        get { return _pathImage; }
        set { SetProperty(ref _pathImage, value); }
    }

    public virtual MessagesFriends MessagesFriends { get; set; }

    /// <summary>
    /// kiểm tra nội dung tin nhắn
    /// </summary>
    /// <returns></returns>
    public bool CheckContentMessage()
    {
        if (Content is not null || NameImage is not null) return true;
        else return false;
    }
}