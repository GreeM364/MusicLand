using MusicLand.WEB.Models.DTO;

namespace MusicLand.WEB.Services.IServices
{
	public interface IAuthenticationService
	{
		Task<T> LoginAsync<T>(LoginDTO objToCreate);
		Task<T> RegisterAsync<T>(RegisterationDTO objToCreate);
        Task<T> ChangePasswordAsync<T>(ChangePasswordDTO changePassword, string token);
    }
}
