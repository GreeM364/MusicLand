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
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _userSongRatingRepository;

        public RatingController(IRatingRepository userSongRatingRepository)
        {
            _userSongRatingRepository = userSongRatingRepository;
        }


        [HttpPost("add")]
        public async Task<IActionResult> AddSongRating(SongRatingViewModel songRating)
        {
            var dbRating = await _userSongRatingRepository.InsertAsync(new SongRating
            {
                UserID = songRating.UserID,
                SongID = songRating.SongID,
                Rating = songRating.Rating
            });

            return Ok(new SongRatingResponseViewModel
            {
                UserID = dbRating.UserID,
                SongID = dbRating.SongID,
                Rating = dbRating.Rating,
                LikedDate = dbRating.LikedDate                
            });
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditSongRating(SongRatingViewModel userSongRating)
        {
            var existing = await _userSongRatingRepository.GetBySongUserIdAsync(userSongRating.SongID, userSongRating.UserID);

            if (existing == null)
                return NotFound(new { Error = "Rating not found" });

            existing.Rating = userSongRating.Rating;

            await _userSongRatingRepository.EditAsync(existing);

            return Ok(new SongRatingResponseViewModel
            {
                UserID = existing.UserID,
                SongID = existing.SongID,
                Rating = existing.Rating,
                LikedDate = existing.LikedDate
            });
        }

        [HttpGet("rating/{songId}")]
		[AllowAnonymous]
		public async Task<IActionResult> GetSongRating(Guid songId)
        {
            var songRating = await _userSongRatingRepository.CalculateSongRatingAsync(songId);

            return Ok(new RatingResponseViewModel
            {
                SongID = songId,
                Rating = songRating
            });
        }

        [HttpGet("ratings/{userId}")]
        public async Task<IActionResult> GetUserSongRatings(Guid userId)
        {
            var userSongRatings = await _userSongRatingRepository.GetByUserIdAsync(userId);

            if (userSongRatings == null || userSongRatings.Count == 0)
                return NotFound(new { Error = "User's ratings for song not found" });

            var response = userSongRatings.Select(x => new SongRatingResponseViewModel
            {
                SongID = x.SongID,
                UserID = x.UserID,
                LikedDate = x.LikedDate,
                Rating = x.Rating
            });

            return Ok(response);
        }


    }
}
