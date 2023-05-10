using Microsoft.AspNetCore.Mvc;
using MusicLand.DAL.Entity;
using MusicLand.DAL.Repositories.IRepository;
using MusicLand.API.Model;
using Microsoft.AspNetCore.Authorization;

namespace MusicLand.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddArtist(ArtistViewModel artist)
        {
            var existing = await _artistRepository.GetByNameAsync(artist.Name);

            if (existing != null)
                return BadRequest(new { Error = "Artist already exist" });

            var dbArtist = await _artistRepository.InsertAsync(new Artist
            {
                Name = artist.Name,
                Country = artist.Country
            }); 

            return Ok(new ArtistResponseViewModel
            {
                ArtistID = dbArtist.ArtistID,
                Country = dbArtist.Country,
                Name = artist.Name
            });         
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditArtist(Guid id, ArtistViewModel artist)
        {
            var existing = await _artistRepository.GetByIdAsync(id);

            if (existing == null)
                return NotFound(new { Error = "Artist not found" });

            existing.Name = artist.Name;
            existing.Country = artist.Country;

            await _artistRepository.EditAsync(existing);

            return Ok(new ArtistResponseViewModel
            {
                ArtistID = existing.ArtistID,
                Country = existing.Country,
                Name = existing.Name
            });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteArtist(Guid id)
        {
            var existing = await _artistRepository.GetByIdAsync(id);

            if (existing == null)
                return NotFound(new { Error = "Artist not found" });

            await _artistRepository.DeleteAsync(id);

            return Ok(new ArtistResponseViewModel
            {
                ArtistID = existing.ArtistID,
                Country = existing.Country,
                Name = existing.Name
            });
        }

        [HttpGet("getAll")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAllArtists()
        {
            var artists = await _artistRepository.GetAllAsync();
            var response = artists.Select(artist => new ArtistResponseViewModel
            {
                ArtistID = artist.ArtistID,
                Country = artist.Country,
                Name = artist.Name
            }).ToList();

            return Ok(response);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetArtist(Guid id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);

            if (artist == null)
                return NotFound(new { Error = "Artist not found" });

            var response = new ArtistResponseViewModel
            {
                ArtistID = artist.ArtistID,
                Country = artist.Country,
                Name = artist.Name
            };

            return Ok(response);
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> GetArtistByName(string name)
        {
            var artist = await _artistRepository.GetByNameAsync(name);

            if (artist == null)
                return NotFound(new { Error = "Artist not found" });

            var response = new ArtistResponseViewModel
            {
                ArtistID = artist.ArtistID,
                Country = artist.Country,
                Name = artist.Name
            };

            return Ok(response);
        }
    }
}
