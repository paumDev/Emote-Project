using ClassLibraryEmotes;
using System.Text.Json;

namespace EmoteApp.App.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient? _httpClient;
        public UserDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var list = await JsonSerializer.DeserializeAsync<IEnumerable<User>>
                    (await _httpClient.GetStreamAsync($"api/User"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return list;
        }
        public async Task<User> GetUserDetails(int idUser)
        {
            return await JsonSerializer.DeserializeAsync<User>
                (await _httpClient.GetStreamAsync($"api/User/{idUser}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
