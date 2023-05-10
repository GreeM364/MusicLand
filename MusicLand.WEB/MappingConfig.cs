using AutoMapper;
using MusicLand.WEB.Models.DTO;

namespace MusicLand.WEB
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
			CreateMap<ArtistDTO, ArtistCreateDTO>().ReverseMap();
			CreateMap<ArtistDTO, ArtistUpdateDTO>().ReverseMap();

			CreateMap<GenreDTO, GenreCreateDTO>().ReverseMap();
			CreateMap<GenreDTO, GenreUpdateDTO>().ReverseMap();

            CreateMap<SongDTO, SongCreateDTO>().ReverseMap();
            CreateMap<SongDTO, SongUpdateDTO>().ReverseMap();
        }
	}
}
