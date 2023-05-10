using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicLand.WEB.Models.DTO;
using MusicLand.WEB.Services.IServices;
using MusicLand.WEB.Utility;

namespace MusicLand.WEB.Controllers
{
	public class GenreController : Controller
	{

		private readonly IGenreService _genreService;
		private readonly IMapper _mapper;

		public GenreController(IGenreService genreService, IMapper mapper)
		{
			_genreService = genreService;
			_mapper = mapper;
		}

		public async Task<IActionResult> IndexGenre()
		{
			var response = await _genreService.GetAllAsync<List<GenreDTO>>(HttpContext.Session.GetString(SD.SessionToken));

			if (response == null)
			{
				return View(new List<GenreDTO>());
			}

			return View(response);
		}

		public ActionResult CreateGenre()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> CreateGenre(GenreCreateDTO artistDTO)
		{
			var response = await _genreService.CreateAsync<GenreDTO>(artistDTO, HttpContext.Session.GetString(SD.SessionToken));

			if (response != null)
			{
				return RedirectToAction(nameof(IndexGenre));
			}

			return View(response);
		}

		public async Task<IActionResult> UpdateGenre(string genreId)
		{
			var response = await _genreService.GetAsync<GenreDTO>(genreId, HttpContext.Session.GetString(SD.SessionToken));
			if (response != null)
			{
				return View(_mapper.Map<GenreUpdateDTO>(response));
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateGenre(GenreUpdateDTO model)
		{
			if (ModelState.IsValid)
			{
				var response = await _genreService.UpdateAsync<GenreDTO>(model, HttpContext.Session.GetString(SD.SessionToken));
				if (response != null)
				{
					return RedirectToAction(nameof(IndexGenre));
				}
			}

			return View(model);
		}

		public async Task<IActionResult> DeleteGenre(string artistId)
		{
			var response = await _genreService.GetAsync<GenreDTO>(artistId, HttpContext.Session.GetString(SD.SessionToken));
			if (response != null)
			{
				return View(response);
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> DeleteGenre(GenreDTO model)
		{
			var response = await _genreService.DeleteAsync<GenreDTO>(model.GenreID, HttpContext.Session.GetString(SD.SessionToken));
			if (response != null)
			{
				return RedirectToAction(nameof(IndexGenre));
			}

			return View(model);
		}
	}
}
