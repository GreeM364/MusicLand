using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicLand.WEB.Services.IServices;
using MusicLand.WEB.Utility;
using MusicLand.WEB.Models.DTO;
using System.Data;
using System.Security.Claims;

namespace MusicLand.WEB.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexArtist()
        {
            var response = await _artistService.GetAllAsync<List<ArtistDTO>>(HttpContext.Session.GetString(SD.SessionToken));

            if (response == null)
            {
                return View(new List<ArtistDTO>());
            }

            return View(response);
        }

        public ActionResult CreateArtist()
        {  
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateArtist(ArtistCreateDTO artistDTO)
        {
            var response = await _artistService.CreateAsync<ArtistDTO>(artistDTO, HttpContext.Session.GetString(SD.SessionToken));

            if (response != null)
            {
                return RedirectToAction(nameof(IndexArtist));
            }

            return View(response);
        }


        public async Task<IActionResult> UpdateArtist(string artistId)
        {
            var response = await _artistService.GetAsync<ArtistDTO>(artistId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null)
            {
                return View(_mapper.Map<ArtistUpdateDTO>(response));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateArtist(ArtistUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _artistService.UpdateAsync<ArtistDTO>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null)
                {
                    return RedirectToAction(nameof(IndexArtist));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteArtist(string artistId)
        {
            var response = await _artistService.GetAsync<ArtistDTO>(artistId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null)
            {
                return View(response);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArtist(ArtistDTO model)
        {
            var response = await _artistService.DeleteAsync<ArtistDTO>(model.ArtistId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null)
            {
                return RedirectToAction(nameof(IndexArtist));
            }

            return View(model);
        }
    }
}
