using Microsoft.EntityFrameworkCore;
using mile3image.Database;
using mile3image.Entities;
using mile3image.IRepository;

namespace mile3image.Repository
{
    public class UserRepository :IUserRepository
    {
        private readonly LibraryDbContext _libraryDbContext;
        public UserRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<User> CreateUser(User user)
        {
            await _libraryDbContext.Users.AddAsync(user);  
            await _libraryDbContext.SaveChangesAsync();
            return user;
        }
        public async Task<List<User>> GetAllUsers()
        {
          var data= await _libraryDbContext.Users.ToListAsync();
            return data;

        }
        public async Task<User> GetUserById(int  userid)
        {
            var user = await _libraryDbContext.Users.FirstOrDefaultAsync(x => x.UserId == userid);
            if (user == null)
            {
                throw new Exception("can't find User");
            }
            else
            {
                return user;
            }
        }

        public async Task UpdateUser(User user)
        {
            _libraryDbContext.Users.Update(user);
            await _libraryDbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _libraryDbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            _libraryDbContext.Users.Remove(user);
            await _libraryDbContext.SaveChangesAsync();
        }
    }
}
