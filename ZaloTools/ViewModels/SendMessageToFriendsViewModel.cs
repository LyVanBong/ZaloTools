namespace ZaloTools.ViewModels
{
    public class SendMessageToFriendsViewModel : RegionViewModelBase
    {
        private readonly IMessagesFriendsRepository _messagesFriendsRepository;

        public SendMessageToFriendsViewModel(IRegionManager regionManager, IMessagesFriendsRepository messagesFriendsRepository) : base(regionManager)
        {
            _messagesFriendsRepository = messagesFriendsRepository;
        }
    }
}