using Prism.Mvvm;

namespace ZaloTools.Models;

public class MenuApp : BindableBase
{
    private bool _about;
    private bool _phoneNumber;
    private bool _acount;
    private bool _dashboard = true;
    private bool _messageGroupChat;
    private bool _messagePhoneNumber;
    private bool _messageFriend;
    private bool _message;
    private bool _addFriendPhoneNumber;
    private bool _addFriendSuggestionsAddFriend;
    private bool _addFriendAcceptAddFriend;
    private bool _addFriend;

    public bool AddFriend
    {
        get => _addFriend;
        set => SetProperty(ref _addFriend, value);
    }

    public bool AddFriendAcceptAddFriend
    {
        get => _addFriendAcceptAddFriend;
        set => SetProperty(ref _addFriendAcceptAddFriend, value);
    }

    public bool AddFriendSuggestionsAddFriend
    {
        get => _addFriendSuggestionsAddFriend;
        set => SetProperty(ref _addFriendSuggestionsAddFriend, value);
    }

    public bool AddFriendPhoneNumber
    {
        get => _addFriendPhoneNumber;
        set => SetProperty(ref _addFriendPhoneNumber, value);
    }
    public bool Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    public bool MessageFriend
    {
        get => _messageFriend;
        set => SetProperty(ref _messageFriend, value);
    }

    public bool MessagePhoneNumber
    {
        get => _messagePhoneNumber;
        set => SetProperty(ref _messagePhoneNumber, value);
    }

    public bool MessageGroupChat
    {
        get => _messageGroupChat;
        set => SetProperty(ref _messageGroupChat, value);
    }
    public bool Dashboard
    {
        get => _dashboard;
        set => SetProperty(ref _dashboard, value);
    }

    public bool Acount
    {
        get => _acount;
        set => SetProperty(ref _acount, value);
    }

    public bool PhoneNumber
    {
        get => _phoneNumber;
        set => SetProperty(ref _phoneNumber, value);
    }

    public bool About
    {
        get => _about;
        set => SetProperty(ref _about, value);
    }

    public void SelectButton(int key = 0)
    {
        Message = false;
        About = false;
        PhoneNumber = false;
        Acount = false;
        Dashboard = false;
        MessageGroupChat = false;
        MessagePhoneNumber = false;
        MessageFriend = false;
        AddFriendPhoneNumber = false;
        AddFriendSuggestionsAddFriend = false;
        AddFriendAcceptAddFriend = false;
        AddFriend = false;
        if (key == 1) Dashboard = true;
        if (key == 2) Acount = true;
        if (key == 3) PhoneNumber = true;
        if (key == 4) About = true;
    }

    public void SelectButtonAddFriend(int key = 0)
    {
        Message = false;
        About = false;
        PhoneNumber = false;
        Acount = false;
        Dashboard = false;
        MessageGroupChat = false;
        MessagePhoneNumber = false;
        MessageFriend = false;
        AddFriendPhoneNumber = false;
        AddFriendSuggestionsAddFriend = false;
        AddFriendAcceptAddFriend = false;
        AddFriend = true;
        if (key == 1) AddFriendAcceptAddFriend = true;
        if (key == 2) AddFriendSuggestionsAddFriend = true;
        if (key == 3) AddFriendPhoneNumber = true;
        if (key == -1) AddFriend = false;

    }
    public void SelectButtonMessage(int key = 0)
    {
        Message = true;
        About = false;
        PhoneNumber = false;
        Acount = false;
        Dashboard = false;
        MessageGroupChat = false;
        MessagePhoneNumber = false;
        MessageFriend = false;
        AddFriendPhoneNumber = false;
        AddFriendSuggestionsAddFriend = false;
        AddFriendAcceptAddFriend = false;
        AddFriend = false;
        if (key == 1) MessageFriend = true;
        if (key == 2) MessagePhoneNumber = true;
        if (key == 3) MessageGroupChat = true;
        if (key == -1) Message = false;
    }
}