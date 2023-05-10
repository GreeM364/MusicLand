using MusicLand.DAL.Entity;

namespace MusicLand.DAL.Repositories.IRepository
{
    public interface ISongRepository
    {
        Task<Song> InsertAsync(Song song);
        Task EditAsync(Song newSong);

        Task DeleteAsync(Guid songId);

        Task<IReadOnlyCollection<Song>> GetAllAsync();

        Task<Song> GetByIdAsync(Guid id);

        Task<List<Song>> GetByArtistIdAsync(Guid artistId);

        Task<List<Song>> GetByGenreIdAsync(Guid genreId);

        Task CreateIndexesAsync();
    }

}

