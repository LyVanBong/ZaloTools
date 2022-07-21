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

        public bool Delete(MessagesFriends messagesFriends)
        {
            _database.MessagesFriends.Remove(messagesFriends);
            var delete = _database.SaveChanges();
            return delete > 0;
        }

        public bool Add(MessagesFriends messagesFriends)
        {
            _database.MessagesFriends.Add(messagesFriends);
            var insert = _database.SaveChanges();
            return insert > 0;
        }

        public bool Update(MessagesFriends messagesFriends)
        {
            _database.MessagesFriends.Update(messagesFriends);
            var update = _database.SaveChanges();
            return update > 0;
        }

        public MessagesFriends Get(Guid Id)
        {
            return _database.MessagesFriends?.FirstOrDefault(x => x.Id == Id);
        }

        public List<MessagesFriends> GetAll()
        {
            return _database.MessagesFriends?.ToList();
        }
    }
}