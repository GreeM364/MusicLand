using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MusicLand.DAL.Entity;
using MusicLand.DAL.Repositories.IRepository;

namespace MusicLand.DAL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IMongoCollection<Genre> _collection;

        public GenreRepository(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("MongoDBConnection");
            _collection = new MongoClient(connString)
                .GetDatabase("MusicLand")
                .GetCollection<Genre>("genres");
        }

        public async Task<Genre> InsertAsync(Genre genre)
        {
            var existingArtist = await GetByNameAsync(genre.Name);
            if (existingArtist != null)
                throw new Exception("Genre with same name already exists");

            genre.GenreID = Guid.NewGuid();
            await _collection.InsertOneAsync(genre);
            return genre;
        }

        public Task EditAsync(Genre newGenre)
        {
            return _collection.ReplaceOneAsync(x => x.GenreID == newGenre.GenreID, newGenre);
        }

        public Task DeleteAsync(Guid genreId)
        {
            return _collection.DeleteOneAsync(x => x.GenreID == genreId);
        }

        public async Task<IReadOnlyCollection<Genre>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public Task<Genre> GetByIdAsync(Guid id)
        {
            return _collection.Find(x => x.GenreID == id).FirstOrDefaultAsync();
        }

        public async Task<Genre> GetByNameAsync(string name)
        {
            return await _collection.Find(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task CreateIndexesAsync()
        {
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<Genre>(Builders<Genre>.IndexKeys.Ascending(x => x.GenreID))).ConfigureAwait(false);
        }
    }
}
