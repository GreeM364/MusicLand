using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicLand.WEB.Models.DTO;

namespace MusicLand.WEB.Models.VM
{
    public class SongCreateViewModel
    {
        public SongCreateDTO Song { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ArtistList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> GenreList { get; set; }
    }
}
