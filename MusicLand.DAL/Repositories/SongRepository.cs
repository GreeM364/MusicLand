using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MusicLand.DAL.Entity;
using MusicLand.DAL.Repositories.IRepository;

namespace MusicLand.DAL.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly IMongoCollection<Song> _collection;

        public SongRepository(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("MongoDBConnection");
            _collection = new MongoClient(connString)
                .GetDatabase("MusicLand")
                .GetCollection<Song>("songs");
        }

        public async Task<Song> InsertAsync(Song song)
        {
            song.SongID = Guid.NewGuid();
            await _collection.InsertOneAsync(song);
            return song;
        }

        public Task EditAsync(Song newSong)
        {
            return _collection.ReplaceOneAsync(x => x.SongID == newSong.SongID, newSong);
        }

        public Task DeleteAsync(Guid songId)
        {
            return _collection.DeleteOneAsync(x => x.SongID == songId);
        }

        public async Task<IReadOnlyCollection<Song>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public Task<Song> GetByIdAsync(Guid id)
        {
            return _collection
                .Find(x => x.SongID == id)
                .FirstOrDefaultAsync();
        }

        public Task<List<Song>> GetByArtistIdAsync(Guid artistId)
        {
            return _collection
                .Find(x => x.ArtistID == artistId)
                .ToListAsync();
        }

        public Task<List<Song>> GetByGenreIdAsync(Guid genreId)
        {
            return _collection
                .Find(x => x.GenreID == genreId)
                .ToListAsync();
        }

        public async Task CreateIndexesAsync()
        {
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<Song>(Builders<Song>.IndexKeys.Ascending(x => x.SongID))).ConfigureAwait(false);
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<Song>(Builders<Song>.IndexKeys.Ascending(x => x.GenreID))).ConfigureAwait(false);
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<Song>(Builders<Song>.IndexKeys.Ascending(x => x.ArtistID))).ConfigureAwait(false);
        }
    }
}
