using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicLand.WEB.Models.DTO;
using MusicLand.WEB.Models.VM;
using MusicLand.WEB.Services.IServices;
using MusicLand.WEB.Utility;

namespace MusicLand.WEB.Controllers
{
	public class SongController : Controller
	{
		private readonly ISongService _songService;
		private readonly IArtistService _artistService;
		private readonly IGenreService _genreService;
        private readonly ISongRatingService _songRatingService;
		private readonly IMapper _mapper;

		public SongController(ISongService songService, IArtistService artistService,
							  IGenreService genreService, ISongRatingService songRatingService, IMapper mapper)
		{
			_songService = songService;
			_artistService = artistService;
			_genreService = genreService;
            _songRatingService = songRatingService;
			_mapper = mapper;
		}

		public async Task<IActionResult> IndexSong()
		{
			var responseSong = await _songService.GetAllAsync<List<SongDTO>>(HttpContext.Session.GetString(SD.SessionToken));
			var responseArtist = await _artistService.GetAllAsync<List<ArtistDTO>>(HttpContext.Session.GetString(SD.SessionToken));
			var responseGenre = await _genreService.GetAllAsync<List<GenreDTO>>(HttpContext.Session.GetString(SD.SessionToken));

			if (responseSong == null)
			{
				return View(new List<SongViewModel>());
			}

            var model = from song in responseSong
                           join artist in responseArtist on song.ArtistID equals artist.ArtistId
                           join genre in responseGenre on song.GenreID equals genre.GenreID
                           select new SongViewModel
                           {
                               SongID = song.SongID,
                               Title = song.Title,
                               Duration = song.Duration,
                               Artist = artist.Name,
                               Genre = genre.Name
                           };

			List<SongViewModel> songViewModelList = model.ToList();
			foreach (var item in songViewModelList)
            {
                item.Rating = (await _songRatingService.GetAsync<RatingResponseDTO>(item.SongID, HttpContext.Session.GetString(SD.SessionToken))).Rating;
            }

            return View(songViewModelList);
		}

        public async Task<IActionResult> CreateSong()
        {
            SongCreateViewModel songCreateVM = new SongCreateViewModel();

            var responseArtist = await _artistService.GetAllAsync<List<ArtistDTO>>(HttpContext.Session.GetString(SD.SessionToken));
            var responseGenre = await _genreService.GetAllAsync< List<GenreDTO>>(HttpContext.Session.GetString(SD.SessionToken));

            if (responseArtist != null && responseGenre != null)
            {
                songCreateVM.ArtistList = responseArtist.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ArtistId
                });

                songCreateVM.GenreList = responseGenre.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.GenreID
                });
            }
            return View(songCreateVM);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSong(SongCreateViewModel songCreateVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _songService.CreateAsync<SongDTO>(songCreateVM.Song, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null)
                {
                    return RedirectToAction(nameof(IndexSong));
                }
                return NotFound();
            }

            var responseArtist = await _artistService.GetAllAsync<List<ArtistDTO>>(HttpContext.Session.GetString(SD.SessionToken));
            var responseGenre = await _genreService.GetAllAsync<List<GenreDTO>>(HttpContext.Session.GetString(SD.SessionToken));
            if (responseArtist != null && responseGenre != null)
            {
                songCreateVM.ArtistList = responseArtist.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ArtistId
                });

                songCreateVM.GenreList = responseGenre.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.GenreID
                });
            }
            return View(songCreateVM);
        }

        public async Task<IActionResult> UpdateSong(string songId)
        {
            SongUpdateViewModel songCreateVM = new SongUpdateViewModel();

            var responseSong = await _songService.GetAsync<SongDTO>(songId, HttpContext.Session.GetString(SD.SessionToken));

            if (responseSong != null )
            {
                songCreateVM.Song = _mapper.Map<SongUpdateDTO>(responseSong);
            }

            var responseArtist = await _artistService.GetAllAsync<List<ArtistDTO>>(HttpContext.Session.GetString(SD.SessionToken));
            var responseGenre = await _genreService.GetAllAsync<List<GenreDTO>>(HttpContext.Session.GetString(SD.SessionToken));
            if (responseArtist != null && responseGenre != null)
            {
                songCreateVM.ArtistList = responseArtist.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ArtistId
                });

                songCreateVM.GenreList = responseGenre.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.GenreID
                });

                return View(songCreateVM);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSong(SongUpdateViewModel songUpdateVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _songService.UpdateAsync<SongUpdateDTO>(songUpdateVM.Song, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null)
                {
                    return RedirectToAction(nameof(IndexSong));
                }
                return NotFound();
            }

            var responseArtist = await _artistService.GetAllAsync<List<ArtistDTO>>(HttpContext.Session.GetString(SD.SessionToken));
            var responseGenre = await _genreService.GetAllAsync<List<GenreDTO>>(HttpContext.Session.GetString(SD.SessionToken));
            if (responseArtist != null && responseGenre != null)
            {
                songUpdateVM.ArtistList = responseArtist.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ArtistId
                });

                songUpdateVM.GenreList = responseGenre.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.GenreID
                });
            }
            return View(songUpdateVM);
        }

        public async Task<IActionResult> DeleteSong(string songId)
        {
			SongDeleteViewModel songCreateVM = new SongDeleteViewModel();

            var responseSong = await _songService.GetAsync<SongDTO>(songId, HttpContext.Session.GetString(SD.SessionToken));

            if (responseSong != null)
            {
                songCreateVM.Song = responseSong;
            }

            var responseArtist = await _artistService.GetAllAsync<List<ArtistDTO>>(HttpContext.Session.GetString(SD.SessionToken));
            var responseGenre = await _genreService.GetAllAsync<List<GenreDTO>>(HttpContext.Session.GetString(SD.SessionToken));
            if (responseArtist != null && responseGenre != null)
            {
                songCreateVM.ArtistList = responseArtist.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ArtistId
                });

                songCreateVM.GenreList = responseGenre.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.GenreID
                });

                return View(songCreateVM);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSong(SongDeleteViewModel model)
        {
            var response = await _songService.DeleteAsync<SongDTO>(model.Song.SongID, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null)
            {
                return RedirectToAction(nameof(IndexSong));
            }

            return View(model);
        }

    }
}
