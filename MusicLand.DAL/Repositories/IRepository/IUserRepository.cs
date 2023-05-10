using MusicLand.DAL.Entity;

namespace MusicLand.DAL.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<User> InsertAsync(User user);
        Task EditAsync(User newUser);
        Task DeleteAsync(Guid userId);
        Task<IReadOnlyCollection<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByUsernameAsync(string email);
        Task<User> GetByUsernameAndPasswordAsync(string email, string password);
        Task CreateIndexesAsync();
    }
}
