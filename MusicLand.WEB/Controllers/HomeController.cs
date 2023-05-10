using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MusicLand.WEB.Models;
using MusicLand.WEB.Models.DTO;
using MusicLand.WEB.Models.VM;
using MusicLand.WEB.Services.IServices;
using MusicLand.WEB.Utility;

namespace MusicLand.WEB.Controllers;

public class HomeController : Controller
{

	private readonly ISongService _songService;
	private readonly IArtistService _artistService;
	private readonly IGenreService _genreService;
	private readonly ISongRatingService _songRatingService;

	public HomeController(ISongService songService, IArtistService artistService,
						  IGenreService genreService, ISongRatingService songRatingService)
	{
		_songService = songService;
		_artistService = artistService;
		_genreService = genreService;
		_songRatingService = songRatingService;
	}

	public async Task<IActionResult> Index()
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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
