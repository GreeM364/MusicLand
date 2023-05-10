using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicLand.API.Model;
using MusicLand.DAL.Entity;
using MusicLand.DAL.Repositories.IRepository;

namespace MusicLand.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGenre(GenreViewModel genre)
        {
            var existing = await _genreRepository.GetByNameAsync(genre.Name);

            if (existing != null)
                return BadRequest(new { Error = "Genre already exists" });

            var dbGenre = await _genreRepository.InsertAsync(new Genre
            {
                Name = genre.Name,
                Description = genre.Description
            });

            return Ok(new GenreResponseViewModel
            {
                GenreID = dbGenre.GenreID,
                Name = dbGenre.Name,
                Description = dbGenre.Description
            });
        }


        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditGenre(Guid id, GenreViewModel genre)
        {
            var existing = await _genreRepository.GetByIdAsync(id);

            if (existing == null)
                return NotFound(new { Error = "Genre not found" });

            existing.Name = genre.Name;
            existing.Description = genre.Description;

            await _genreRepository.EditAsync(existing);

            return Ok(new GenreResponseViewModel
            {
                GenreID = existing.GenreID,
                Name = existing.Name,
                Description = existing.Description
            });
        }

        [HttpDelete("delete/{genreId}")]
        public async Task<IActionResult> DeleteGenre(Guid genreId)
        {
            var genreToDelete = await _genreRepository.GetByIdAsync(genreId);
            if (genreToDelete == null)
                return NotFound();

            await _genreRepository.DeleteAsync(genreId);

            return Ok(new GenreResponseViewModel
            {
                GenreID = genreToDelete.GenreID,
                Name = genreToDelete.Name,
                Description = genreToDelete.Description
            });
        }

        [HttpGet("getAll")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreRepository.GetAllAsync();
            var response = genres.Select(genre => new GenreResponseViewModel
            {
                GenreID = genre.GenreID,
                Name = genre.Name,
                Description = genre.Description
            }).ToList();

            return Ok(response);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetGenreById(Guid id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            if (genre == null)
                return NotFound();

            var response = new GenreResponseViewModel
            {
                GenreID = genre.GenreID,
                Name = genre.Name,
                Description = genre.Description
            };

            return Ok(response);
        }

        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> GetGenreByName(string name)
        {
            var genre = await _genreRepository.GetByNameAsync(name);

            if (genre == null)
                return NotFound();

            var response = new GenreResponseViewModel
            {
                GenreID = genre.GenreID,
                Name = genre.Name,
                Description = genre.Description
            };

            return Ok(response);
        }
    }
}
