namespace ZaloTools.ViewModels
{
    public class SendMessageToFriendsViewModel : BindableBase
    {
        public SendMessageToFriendsViewModel()
        {
            var img = Clipboard.GetImage();
        }
    }
}
