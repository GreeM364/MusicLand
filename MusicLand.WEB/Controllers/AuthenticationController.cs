using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MusicLand.WEB.Utility;
using System.Security.Claims;
using MusicLand.WEB.Models.DTO;
using IAuthenticationService = MusicLand.WEB.Services.IServices.IAuthenticationService;

namespace MusicLand.WEB.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly IAuthenticationService _authenticationService;
		public AuthenticationController(IAuthenticationService authService)
		{
			_authenticationService = authService;
		}

		[HttpGet]
		public IActionResult Login()
		{
			LoginDTO obj = new LoginDTO();
			return View(obj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginDTO obj)
		{
			UserTokenDTO response = await _authenticationService.LoginAsync<UserTokenDTO>(obj);
			if (response != null)
			{
				var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
				identity.AddClaim(new Claim(ClaimTypes.Email, response.Email));
				identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
				identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, response.Id.ToString()));
				var principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				HttpContext.Session.SetString(SD.SessionToken, response.Token);

                return RedirectToAction("Index", "Home");             
            }
			else
			{
				ModelState.AddModelError("CustomError", "Error");
				return View(obj);
			}
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterationDTO obj)
		{
			UserDTO result = await _authenticationService.RegisterAsync<UserDTO>(obj);
			if (result != null)
			{
				return RedirectToAction("Login");
			}
			return View();
		}

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePassword)
        {
            await _authenticationService.ChangePasswordAsync<ChangePasswordDTO>(changePassword, HttpContext.Session.GetString(SD.SessionToken));

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			HttpContext.Session.SetString(SD.SessionToken, "");

			return RedirectToAction("Index", "Home");
		}

		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
