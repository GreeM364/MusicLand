using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicLand.WEB.Models.DTO;
using MusicLand.WEB.Models.VM;
using MusicLand.WEB.Services.IServices;
using MusicLand.WEB.Utility;
using System.Security.Claims;

namespace MusicLand.WEB.Controllers
{
    public class RatingController : Controller
    {

        private readonly ISongRatingService _songRatingService;
        private readonly ISongService _songService;
        private readonly IArtistService _artistService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public RatingController(ISongRatingService songRatingService, ISongService songService,
                                IArtistService artistService, IGenreService genreService, IMapper mapper)
        {
            _songRatingService = songRatingService;
            _songService = songService;
            _artistService = artistService;
            _genreService = genreService;   
            _mapper = mapper;
        }

        public async Task<IActionResult> CreateRating(string songID)
        {
            RatingCreateViewModel ratingCreate = new RatingCreateViewModel();
            var UserID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var responseSong = await _songService.GetAsync<SongDTO>(songID, HttpContext.Session.GetString(SD.SessionToken));
            var responseRatings = await _songRatingService.GetAsyncUserRating<List<SongRatingDTO>>(UserID, HttpContext.Session.GetString(SD.SessionToken));
            var responseRating = responseRatings.FirstOrDefault(s => s.SongID == songID);

            if (responseSong != null)
            {
                ratingCreate.Song = responseSong;

            }

            if (responseRating != null)
            {
                ratingCreate.SongRating = responseRating;
            }

            var responseArtist = await _artistService.GetAllAsync<List<ArtistDTO>>(HttpContext.Session.GetString(SD.SessionToken));
            var responseGenre = await _genreService.GetAllAsync<List<GenreDTO>>(HttpContext.Session.GetString(SD.SessionToken));
            if (responseArtist != null && responseGenre != null)
            {
                ratingCreate.ArtistList = responseArtist.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ArtistId
                });

                ratingCreate.GenreList = responseGenre.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.GenreID
                });
            }

            return View(ratingCreate);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRating(RatingCreateViewModel ratingCreateVM)
        {
            var UserID = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var responseRatings = await _songRatingService.GetAsyncUserRating<List<SongRatingDTO>>(UserID, HttpContext.Session.GetString(SD.SessionToken));
            var responseRating = responseRatings.FirstOrDefault(s => s.SongID == ratingCreateVM.Song.SongID);


            if(responseRating != null)
            {
                SongRatingUpdateDTO songRatingUpdateDTO = new SongRatingUpdateDTO()
                {
                    Rating = ratingCreateVM.SongRating.Rating,
                    SongID = ratingCreateVM.Song.SongID,
                    UserID = UserID
                };

                var response = await _songRatingService.UpdateAsync<SongRatingDTO>(songRatingUpdateDTO, HttpContext.Session.GetString(SD.SessionToken));

                if (response != null)
                {
                    return RedirectToAction("IndexSong", "Song");
                }

                return View(response);
            }
            else
            {
                SongRatingCreateDTO songRatingCreateDTO = new SongRatingCreateDTO()
                {
                    Rating = ratingCreateVM.SongRating.Rating,
                    SongID = ratingCreateVM.Song.SongID,
                    UserID = UserID
                };

                var response = await _songRatingService.CreateAsync<SongRatingDTO>(songRatingCreateDTO, HttpContext.Session.GetString(SD.SessionToken));

                if (response != null)
                {
                    return RedirectToAction("IndexSong", "Song");
                }

                return View(response);

            }
        }
    }
}
