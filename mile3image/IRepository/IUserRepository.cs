using mile3image.Entities;

namespace mile3image.IRepository
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
         Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int userid);
        Task UpdateUser(User user);
        Task DeleteUser(int userId);
    }
}
