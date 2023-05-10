using MusicLand.WEB.Models.DTO;

namespace MusicLand.WEB.Services.IServices
{
    public interface ISongRatingService
    {
        Task<T> GetAsync<T>(string id, string token);
        Task<T> GetAsyncUserRating<T>(string id, string token);
        Task<T> CreateAsync<T>(SongRatingCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(SongRatingUpdateDTO dto, string token);
    }
}
