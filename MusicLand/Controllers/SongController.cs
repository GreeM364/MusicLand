using Microsoft.AspNetCore.Mvc;
using MusicLand.DAL.Entity;
using MusicLand.API.Model;
using MusicLand.DAL.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace MusicLand.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SongController : ControllerBase
    {
        private readonly ISongRepository _songRepository;
        public SongController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSong(SongViewModel song)
        {
            var dbSong = await _songRepository.InsertAsync(new Song
            {
                Title = song.Title,
                Duration = song.Duration,
                GenreID = song.GenreID,
                ArtistID = song.ArtistID
            });

            return Ok(new SongResponseViewModel
            {
                SongID = dbSong.SongID,
                Title = song.Title,
                Duration = song.Duration,
                GenreID = song.GenreID,
                ArtistID = song.ArtistID
            });
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditSong(Guid id, SongViewModel song)
        {
            var existing = await _songRepository.GetByIdAsync(id);
            if (existing == null)
                return BadRequest(new { Error = "Song not found" });

            existing.Title = song.Title;
            existing.Duration = song.Duration;
            existing.GenreID = song.GenreID;
            existing.ArtistID = song.ArtistID;

            await _songRepository.EditAsync(existing);

            return Ok(new SongResponseViewModel
            {
                SongID = existing.SongID,
                Title = existing.Title,
                Duration = existing.Duration,
                GenreID = existing.GenreID,
                ArtistID = existing.ArtistID
            });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSong(Guid id)
        {
            var existingSong = await _songRepository.GetByIdAsync(id);

            if (existingSong == null)
                return NotFound();

            await _songRepository.DeleteAsync(id);
            return Ok(new SongResponseViewModel
            {
                SongID = existingSong.SongID,
                Title = existingSong.Title,
                Duration = existingSong.Duration,
                GenreID = existingSong.GenreID,
                ArtistID = existingSong.ArtistID
            });
        }


        [HttpGet("getAll")]
		[AllowAnonymous]
		public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _songRepository.GetAllAsync();
            var response = songs.Select(song => new SongResponseViewModel
            {
                SongID = song.SongID,
                Title = song.Title,
                Duration = song.Duration,
                GenreID = song.GenreID,
                ArtistID = song.ArtistID
            }).ToList();

            return Ok(response);
        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetSongById(Guid id)
        {
            var song = await _songRepository.GetByIdAsync(id);

            if (song == null)
                return NotFound();

            var response = new SongResponseViewModel
            {
                SongID = song.SongID,
                Title = song.Title,
                Duration = song.Duration,
                GenreID = song.GenreID,
                ArtistID = song.ArtistID
            };

            return Ok(response);
        }


        [HttpGet("getByArtistId/{artistId}")]
        public async Task<IActionResult> GetSongsByArtistId(Guid artistId)
        {
            var songs = await _songRepository.GetByArtistIdAsync(artistId);
            var response = songs.Select(song => new SongResponseViewModel
            {
                SongID = song.SongID,
                Title = song.Title,
                Duration = song.Duration,
                GenreID = song.GenreID,
                ArtistID = song.ArtistID
            }).ToList();

            return Ok(response);
        }

        [HttpGet("getByGenreId/{genreId}")]
        public async Task<IActionResult> GetSongsByGenre(Guid genreId)
        {
            var songs = await _songRepository.GetByGenreIdAsync(genreId);
            var response = songs.Select(song => new SongResponseViewModel
            {
                SongID = song.SongID,
                Title = song.Title,
                Duration = song.Duration,
                ArtistID = song.ArtistID,
                GenreID = song.GenreID
            }).ToList();

            return Ok(response);
        }

    }
}
