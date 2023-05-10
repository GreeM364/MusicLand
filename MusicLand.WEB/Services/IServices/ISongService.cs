using MusicLand.WEB.Models.DTO;

namespace MusicLand.WEB.Services.IServices
{
	public interface ISongService
	{
		Task<T> GetAllAsync<T>(string token);
		Task<T> GetAsync<T>(string id, string token);
		Task<T> CreateAsync<T>(SongCreateDTO dto, string token);
		Task<T> UpdateAsync<T>(SongUpdateDTO dto, string token);
		Task<T> DeleteAsync<T>(string id, string token);
	}
}
