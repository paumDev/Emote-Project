using System.Diagnostics.Metrics;
using ClassLibraryEmotes;

namespace BethanysPieShopHRM.Api.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _appDbContext.Users;
        }

        public User GetUserById(int userID)
        {
            return _appDbContext.Users.FirstOrDefault(c => c.UserId == userID);
        }
    }
}
