namespace ZaloTools.Models;

public class Friend : BindableBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private bool _isSelected;

    public bool IsSelected
    {
        get { return _isSelected; }
        set { SetProperty(ref _isSelected, value); }
    }

    private string _name;

    public string Name
    {
        get { return _name; }
        set { SetProperty(ref _name, value); }
    }

    private string _status;

    public string Status
    {
        get { return _status; }
        set { SetProperty(ref _status, value); }
    }

    public virtual MessagesFriends MessagesFriends { get; set; }
}