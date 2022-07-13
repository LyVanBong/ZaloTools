namespace ZaloTools.Models;

public class AccountZalo : BindableBase
{
    private string _name;
    private string _phoneNumber;
    private int _numberOfFriend;
    private string _status;
    private string _running;
    private bool _checkZalo;
    public Guid Id { get; set; }
    [NotMapped]
    public bool CheckZalo
    {
        get => _checkZalo;
        set => SetProperty(ref _checkZalo, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set => SetProperty(ref _phoneNumber, value);
    }

    public int NumberOfFriend
    {
        get => _numberOfFriend;
        set => SetProperty(ref _numberOfFriend, value);
    }

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public string Running
    {
        get => _running;
        set => SetProperty(ref _running, value);
    }

    public string PathProfileChrome { get; set; }
    public DateTime CreateDate { get; } = DateTime.Now;
    public bool CheckAccountZalo()
    {
        if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(PhoneNumber))
            return false;
        return true;
    }
}