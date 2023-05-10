﻿using System.ComponentModel.DataAnnotations;

namespace MusicLand.WEB.Models.DTO
{
	public class SongUpdateDTO
	{
		[Required]
		public string SongID { get; set; } = null!;

		[Required]
		public string Title { get; set; } = null!;

		[Required]
		public int Duration { get; set; }

		[Required]
		public string GenreID { get; set; } = null!;

		[Required]
		public string ArtistID { get; set; } = null!;
	}
}
