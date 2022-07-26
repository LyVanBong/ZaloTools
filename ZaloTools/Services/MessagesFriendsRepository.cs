namespace ZaloTools.Services
{
    internal class MessagesFriendsRepository : IMessagesFriendsRepository
    {
        private DatabaseLocalContext _database;

        public MessagesFriendsRepository(DatabaseLocalContext database)
        {
            _database = database;
            _database.Database.EnsureCreated();
            _database.MessagesFriends.Load();
        }

        public bool Exist(Guid Id)
        {
            return _database.MessagesFriends.Any(x => x.Id == Id);
        }

        public bool Delete(MessagesFriend messagesFriend)
        {
            _database.MessagesFriends.Remove(messagesFriend);
            var delete = _database.SaveChanges();
            return delete > 0;
        }

        public bool Add(MessagesFriend messagesFriend)
        {
            _database.MessagesFriends.Add(messagesFriend);
            var insert = _database.SaveChanges();
            return insert > 0;
        }

        public bool Update(MessagesFriend messagesFriend)
        {
            _database.MessagesFriends.Update(messagesFriend);
            var update = _database.SaveChanges();
            return update > 0;
        }

        public MessagesFriend Get(Guid Id)
        {
            return _database.MessagesFriends?.FirstOrDefault(x => x.Id == Id);
        }

        public List<MessagesFriend> GetAll(int messageType)
        {
            return _database.MessagesFriends?.Where(x => x.MessageType == messageType)?.ToList();
        }
    }
}