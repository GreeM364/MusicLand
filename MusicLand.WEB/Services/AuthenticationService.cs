using MusicLand.WEB.Models;
using MusicLand.WEB.Models.DTO;
using MusicLand.WEB.Utility;
using MusicLand.WEB.Services.IServices;

namespace MusicLand.WEB.Services
{
	public class AuthenticationService : BaseService, IAuthenticationService
	{
		private readonly IHttpClientFactory _clientFactory;
		private string villaUrl;


		public AuthenticationService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
		{
			_clientFactory = clientFactory;
			villaUrl = configuration.GetValue<string>("ServiceUrls:MusicLandAPI");

		}

        public Task<T> LoginAsync<T>(LoginDTO obj)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = obj,
				Url = villaUrl + "/api/User/login"
			});
		}

		public Task<T> RegisterAsync<T>(RegisterationDTO obj)
		{
			return SendAsync<T>(new APIRequest()
			{
				ApiType = SD.ApiType.POST,
				Data = obj,
				Url = villaUrl + "/api/User/register"
			});
		}

        public Task<T> ChangePasswordAsync<T>(ChangePasswordDTO changePasswordDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = changePasswordDTO,
                Url = villaUrl + "/api/User/changePassword",
				Token = token
            });
        }
    }
}

