using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicLand.DAL.Repositories;

namespace MusicLand.DAL.Infrastructure
{
    public static class HostExtensions
    {
        public static async Task CreateIndexesForCollectionsAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
            await userRepository.CreateIndexesAsync();

            var songRepository = scope.ServiceProvider.GetRequiredService<SongRepository>();    
            await songRepository.CreateIndexesAsync();

            var artistRepository = scope.ServiceProvider.GetRequiredService<ArtistRepository>();
            await artistRepository.CreateIndexesAsync();

            var genreRepository = scope.ServiceProvider.GetRequiredService<GenreRepository>();
            await genreRepository.CreateIndexesAsync();
        }
    }
}
