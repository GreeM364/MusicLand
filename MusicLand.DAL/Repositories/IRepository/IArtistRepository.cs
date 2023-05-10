using MusicLand.DAL.Entity;

namespace MusicLand.DAL.Repositories.IRepository
{
    public interface IArtistRepository
    {
        Task<Artist> InsertAsync(Artist artist);
        Task EditAsync(Artist newArtist);
        Task DeleteAsync(Guid artistId);
        Task<IReadOnlyCollection<Artist>> GetAllAsync();
        Task<Artist> GetByIdAsync(Guid id);
        Task<Artist> GetByNameAsync(string name);
        Task CreateIndexesAsync();
    }
}
