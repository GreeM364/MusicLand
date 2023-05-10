using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicLand.API.Model;
using MusicLand.DAL.Entity;
using MusicLand.DAL.Infrastructure;
using MusicLand.DAL.Repositories.IRepository;
using System.Security.Claims;

namespace MusicLand.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHandler _jwtHandler;
        public UserController(IUserRepository userRepository, JwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            var existing = await _userRepository.GetByUsernameAsync(model.Email);

            if (existing != null)
                return BadRequest(new { Error = "User already exist" });

            var dbUser = await _userRepository.InsertAsync(new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
            });

            return Ok(new UserResponseViewModel
            {
                Id = dbUser.UserID,
                Email = dbUser.Email,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
            });
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _userRepository.GetAllAsync();
            var users = entities.Select(x => new UserResponseViewModel
            {
                Id = x.UserID,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName
            });

            return Ok(users);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel model)
        {
            var user = await _userRepository.GetByUsernameAndPasswordAsync(model.Email, model.Password);
           
            if (user == null)
                return BadRequest(new { Error = "User not exists" });

            string token = _jwtHandler.GenerateJwtToken(user); 

            return Ok(new UserTokenViewModel
            {
                Id = user.UserID,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            });
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (model.NewPassword == model.OldPassword)
                return BadRequest("New password must be different from old one");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userToUpdate = await _userRepository.GetByIdAsync(Guid.Parse(userId));
            if (userToUpdate == null)
                return BadRequest(new { Error = "User not exists" });

            userToUpdate.Password = model.NewPassword;
            await _userRepository.EditAsync(userToUpdate);

            return NoContent();
        }
    }
}

