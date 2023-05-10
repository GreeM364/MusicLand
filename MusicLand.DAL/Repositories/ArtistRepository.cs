using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MusicLand.DAL.Entity;
using MusicLand.DAL.Repositories.IRepository;

namespace MusicLand.DAL.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly IMongoCollection<Artist> _collection;
        public ArtistRepository(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("MongoDBConnection");
            _collection = new MongoClient(connString)
                .GetDatabase("MusicLand")
                .GetCollection<Artist>("artists");
        }

        public async Task<Artist> InsertAsync(Artist artist)
        {
            var existingArtist = await GetByNameAsync(artist.Name);
            if (existingArtist != null)
                throw new Exception("Artist with same name already exists");

            artist.ArtistID = Guid.NewGuid();
            await _collection.InsertOneAsync(artist);
            return artist;
        }

        public Task EditAsync(Artist newArtist)
        {
            return _collection.ReplaceOneAsync(x => x.ArtistID == newArtist.ArtistID, newArtist);
        }

        public Task DeleteAsync(Guid artistId)
        {
            return _collection.DeleteOneAsync(x => x.ArtistID == artistId);
        }

        public async Task<IReadOnlyCollection<Artist>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<Artist> GetByIdAsync(Guid id)
        {
            return await _collection.Find(x => x.ArtistID == id).FirstOrDefaultAsync();
        }

        public async Task<Artist> GetByNameAsync(string name)
        {
            return await _collection.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task CreateIndexesAsync()
        {
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<Artist>(Builders<Artist>.IndexKeys.Ascending(x => x.ArtistID))).ConfigureAwait(false);
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<Artist>(Builders<Artist>.IndexKeys.Ascending(x => x.Name))).ConfigureAwait(false);
        }
    }
}
