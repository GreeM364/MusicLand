﻿namespace MusicLand.API.Model
{
	public class UserTokenViewModel
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; } 
		public string Email { get; set; } 
		public string Token { get; set; } 
	}
}
