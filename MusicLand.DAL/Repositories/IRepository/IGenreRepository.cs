using MusicLand.DAL.Entity;

namespace MusicLand.DAL.Repositories.IRepository
{
    public interface IGenreRepository
    {
        Task<Genre> InsertAsync(Genre genre);
        Task EditAsync(Genre newGenre);
        Task DeleteAsync(Guid genreId);
        Task<IReadOnlyCollection<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(Guid id);
        Task<Genre> GetByNameAsync(string name);
        Task CreateIndexesAsync();
    }
}
