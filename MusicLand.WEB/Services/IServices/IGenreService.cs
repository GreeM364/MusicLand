using MusicLand.WEB.Models.DTO;

namespace MusicLand.WEB.Services.IServices
{
	public interface IGenreService
	{
		Task<T> GetAllAsync<T>(string token);
		Task<T> GetAsync<T>(string id, string token);
		Task<T> CreateAsync<T>(GenreCreateDTO dto, string token);
		Task<T> UpdateAsync<T>(GenreUpdateDTO dto, string token);
		Task<T> DeleteAsync<T>(string id, string token);
	}
}
