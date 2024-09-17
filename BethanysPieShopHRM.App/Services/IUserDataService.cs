using ClassLibraryEmotes;

namespace BethanysPieShopHRM.App.Services
{
    public interface IUserDataService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserDetails(int idUser);
    }
}
