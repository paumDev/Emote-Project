using System.Diagnostics.Metrics;
using ClassLibraryEmotes;

namespace BethanysPieShopHRM.Api.Models
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userID);
    }
}
