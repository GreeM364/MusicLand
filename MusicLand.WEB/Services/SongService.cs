using MusicLand.WEB.Models.DTO;
using MusicLand.WEB.Models;
using MusicLand.WEB.Services.IServices;
using MusicLand.WEB.Utility;

namespace MusicLand.WEB.Services
{
	public class SongService : BaseService, ISongService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string MusicLandUrl;

		public SongService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			MusicLandUrl = configuration.GetValue<string>("ServiceUrls:MusicLandAPI");
		}


		public Task<T> CreateAsync<T>(SongCreateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = dto,
				Url = MusicLandUrl + "/api/Song/add",
				Token = token
			});
		}

		public Task<T> DeleteAsync<T>(string id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.DELETE,
				Url = MusicLandUrl + "/api/Song/delete/" + id,
				Token = token
			});
		}

		public Task<T> GetAllAsync<T>(string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = MusicLandUrl + "/api/Song/getAll",
				Token = token
			});
		}

		public Task<T> GetAsync<T>(string id, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.GET,
				Url = MusicLandUrl + "/api/Song/get/" + id,
				Token = token
			});
		}

		public Task<T> UpdateAsync<T>(SongUpdateDTO dto, string token)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.PUT,
				Data = dto,
				Url = MusicLandUrl + "/api/Song/edit/" + dto.SongID,
				Token = token
			});
		}
	}
}
