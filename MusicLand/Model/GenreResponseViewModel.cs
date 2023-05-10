namespace MusicLand.API.Model
{
    public class GenreResponseViewModel
    {
        public Guid GenreID { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
