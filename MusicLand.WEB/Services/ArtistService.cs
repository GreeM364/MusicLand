using MusicLand.WEB.Models;
using MusicLand.WEB.Models.DTO;
using MusicLand.WEB.Services.IServices;
using MusicLand.WEB.Utility;

namespace MusicLand.WEB.Services
{
    public class ArtistService : BaseService, IArtistService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string MusicLandUrl;

        public ArtistService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            MusicLandUrl = configuration.GetValue<string>("ServiceUrls:MusicLandAPI");
        }

        public Task<T> CreateAsync<T>(ArtistCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = MusicLandUrl + "/api/Artist/add",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(string id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = MusicLandUrl + "/api/Artist/delete/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = MusicLandUrl + "/api/Artist/getAll",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(string id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = MusicLandUrl + "/api/Artist/get/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(ArtistUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = MusicLandUrl + "/api/Artist/edit/" + dto.ArtistId,
                Token = token
            });
        }
    }
}
