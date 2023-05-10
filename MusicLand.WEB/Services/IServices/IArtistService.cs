using MusicLand.WEB.Models.DTO;

namespace MusicLand.WEB.Services.IServices
{
    public interface IArtistService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(string id, string token);
        Task<T> CreateAsync<T>(ArtistCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(ArtistUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(string id, string token);
    }
}
