using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MusicLand.DAL.Repositories;
using MusicLand.DAL.Repositories.IRepository;
using System.Text;

namespace MusicLand.DAL.Infrastructure
{
    public static class DataAccessLayerExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<JwtHandler>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ISongRepository, SongRepository>();
            services.AddSingleton<IArtistRepository, ArtistRepository>();
            services.AddSingleton<IGenreRepository, GenreRepository>();
            services.AddSingleton<IRatingRepository, RatingRepository>();

            var key = configuration.GetValue<string>("ApiSettings:Secret");

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
