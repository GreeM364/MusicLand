using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicLand.WEB.Models.DTO;

namespace MusicLand.WEB.Models.VM
{
    public class RatingCreateViewModel
    {
        public SongDTO Song { get; set; }
        public SongRatingDTO SongRating { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ArtistList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> GenreList { get; set; }

        public IEnumerable<SelectListItem> RatingList = new List<SelectListItem>()
        {
            new SelectListItem(){ Value = 1.ToString(), Text = 1.ToString()},
            new SelectListItem(){ Value = 2.ToString(), Text = 2.ToString()},
            new SelectListItem(){ Value = 3.ToString(), Text = 3.ToString()},
            new SelectListItem(){ Value = 4.ToString(), Text = 4.ToString()},
            new SelectListItem(){ Value = 5.ToString(), Text = 5.ToString()}
        };
    }
}
