using MusicLand.DAL.Entity;

namespace MusicLand.DAL.Repositories.IRepository
{
    public interface IRatingRepository
    {
        Task<SongRating> InsertAsync(SongRating userSongRating);
        Task EditAsync(SongRating newUserSongRating);
        Task<SongRating> GetBySongUserIdAsync(Guid songId, Guid userId);
        Task<IReadOnlyCollection<SongRating>> GetBySongIdAsync(Guid songId);
        Task<IReadOnlyCollection<SongRating>> GetByUserIdAsync(Guid userId);
        Task<double> CalculateSongRatingAsync(Guid songId);
        Task CreateIndexesAsync();
    }
}
