using MusicLand.WEB.Models.DTO;
using MusicLand.WEB.Models;
using MusicLand.WEB.Services.IServices;
using MusicLand.WEB.Utility;

namespace MusicLand.WEB.Services
{
    public class SongRatingService : BaseService, ISongRatingService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string MusicLandUrl;

        public SongRatingService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            MusicLandUrl = configuration.GetValue<string>("ServiceUrls:MusicLandAPI");
        }

        public Task<T> CreateAsync<T>(SongRatingCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = MusicLandUrl + "/api/Rating/add",
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(SongRatingUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = MusicLandUrl + "/api/Rating/edit/",
                Token = token
            });
        }


        public Task<T> GetAsync<T>(string id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = MusicLandUrl + "/api/Rating/rating/" + id,
                Token = token
            });
        }

        public Task<T> GetAsyncUserRating<T>(string id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = MusicLandUrl + "/api/Rating/ratings/" + id,
                Token = token
            });
        }
    }
}
