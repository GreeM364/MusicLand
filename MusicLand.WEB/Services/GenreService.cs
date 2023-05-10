using MusicLand.WEB.Models.DTO;
using MusicLand.WEB.Models;
using MusicLand.WEB.Services.IServices;
using MusicLand.WEB.Utility;

namespace MusicLand.WEB.Services
{
	public class GenreService : BaseService, IGenreService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string MusicLandUrl;

		public GenreService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			MusicLandUrl = configuration.GetValue<string>("ServiceUrls:MusicLandAPI");
		}

		public Task<T> CreateAsync<T>(GenreCreateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url = MusicLandUrl + "/api/Genre/add",
				Token = token
			});
		}

		public Task<T> DeleteAsync<T>(string id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = MusicLandUrl + "/api/Genre/delete/" + id,
				Token = token
			});
		}

		public Task<T> GetAllAsync<T>(string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = MusicLandUrl + "/api/Genre/getAll",
				Token = token
			});
		}

		public Task<T> GetAsync<T>(string id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = MusicLandUrl + "/api/Genre/get/" + id,
				Token = token
			});
		}

		public Task<T> UpdateAsync<T>(GenreUpdateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				Url = MusicLandUrl + "/api/Genre/edit/" + dto.GenreID,
				Token = token
			});
		}
	}
}
