using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MusicLand.DAL.Entity;
using MusicLand.DAL.Repositories.IRepository;

namespace MusicLand.DAL.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IMongoCollection<SongRating> _collection;
        public RatingRepository(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("MongoDBConnection");
            _collection = new MongoClient(connString)
                .GetDatabase("MusicLand")
                .GetCollection<SongRating>("userSongRatings");
        }

        public async Task<SongRating> InsertAsync(SongRating userSongRating)
        {
            userSongRating.Id = Guid.NewGuid();
            userSongRating.LikedDate = DateTime.Now;

            await _collection.InsertOneAsync(userSongRating);
            return userSongRating;
        }

        public Task EditAsync(SongRating newUserSongRating)
        {
            newUserSongRating.LikedDate = DateTime.Now;
            return _collection.ReplaceOneAsync(x => x.UserID == newUserSongRating.UserID && x.SongID == newUserSongRating.SongID, newUserSongRating);
        }

        public async Task<SongRating> GetBySongUserIdAsync(Guid songId, Guid userId)
        {
            return await _collection.Find(x => x.UserID == userId && x.SongID == songId).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<SongRating>> GetBySongIdAsync(Guid songId)
        {
            return await _collection.Find(x => x.SongID == songId).ToListAsync();
        }

        public async Task<IReadOnlyCollection<SongRating>> GetByUserIdAsync(Guid userId)
        {
            return await _collection.Find(x => x.UserID == userId).ToListAsync();
        }

        public async Task<double> CalculateSongRatingAsync(Guid songId)
        {
            var userSongRatings = await GetBySongIdAsync(songId);

            if (userSongRatings == null || userSongRatings.Count == 0)
                return 0;

            var totalRating = userSongRatings.Sum(x => x.Rating);
            var avgRating = (double)totalRating / userSongRatings.Count;

            return avgRating;
        }

        public async Task CreateIndexesAsync()
        {
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<SongRating>(Builders<SongRating>.IndexKeys.Ascending(x => x.SongID))).ConfigureAwait(false);
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<SongRating>(Builders<SongRating>.IndexKeys.Ascending(x => x.UserID))).ConfigureAwait(false);
        }
    }
}
