using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MusicLand.DAL.Entity;
using MusicLand.DAL.Repositories.IRepository;

namespace MusicLand.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("MongoDBConnection");
            _collection = new MongoClient(connString)
                .GetDatabase("MusicLand")
                .GetCollection<User>("users");
        }

        public async Task<User> InsertAsync(User user)
        {
            var existingUser = await GetByUsernameAsync(user.Email);
            if (existingUser != null)
                throw new Exception("User with same email already exists");

            user.UserID = Guid.NewGuid();
            await _collection.InsertOneAsync(user);
            return user;
        }

        public Task EditAsync(User newUser)
        {
            return _collection.ReplaceOneAsync(x => x.UserID == newUser.UserID, newUser);
        }

        public Task DeleteAsync(Guid userId)
        {
            return _collection.DeleteOneAsync(x => x.UserID == userId);
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _collection.Find(x => x.UserID == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByUsernameAsync(string email)
        {
            return await _collection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetByUsernameAndPasswordAsync(string email, string password)
        {
            return await _collection.Find(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }

        public async Task CreateIndexesAsync()
        {
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(x => x.UserID))).ConfigureAwait(false);
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(x => x.Email))).ConfigureAwait(false);
        }
    }
}
