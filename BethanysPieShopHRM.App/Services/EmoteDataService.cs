using Blazored.LocalStorage;
using ClassLibraryEmotes;
using System.Net.Http.Json;
using System.Text.Json;
using BethanysPieShopHRM.App.Helper;
using System.Text;

namespace BethanysPieShopHRM.App.Services
{
    public class EmoteDataService : IEmoteDataService
    {
        private readonly HttpClient? _httpClient;
        private readonly ILocalStorageService _localStorageService;
        public EmoteDataService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<Emote> AddEmote(Emote emote)
        {
            var emoteJson =
                new StringContent(JsonSerializer.Serialize(emote), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Emote", emoteJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Emote>(await response.Content.ReadAsStreamAsync());
            }
            return null;
        }
        public async Task DeleteEmote(int idEmote)
        {
            await _httpClient.DeleteAsync($"api/Emote/{idEmote}");
        }
        public async Task<IEnumerable<Emote>> GetAllEmotes(bool refreshRequired = false)
        {
            if (refreshRequired)
            {
                bool emoteExpirationExists = await _localStorageService.ContainKeyAsync(LocalStorageConstants.EmotesListExpirationKey);
                if (emoteExpirationExists)
                {
                    DateTime emoteListExpiration = await _localStorageService.GetItemAsync<DateTime>(LocalStorageConstants.EmotesListExpirationKey);
                    if (emoteListExpiration > DateTime.Now)//get from local storage
                    {
                        if (await _localStorageService.ContainKeyAsync(LocalStorageConstants.EmotesListKey))
                        {
                            return await _localStorageService.GetItemAsync<List<Emote>>(LocalStorageConstants.EmotesListKey);
                        }
                    }
                }
            }

            var list = await JsonSerializer.DeserializeAsync<IEnumerable<Emote>>
                    (await _httpClient.GetStreamAsync($"api/Emote"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            await _localStorageService.SetItemAsync(LocalStorageConstants.EmotesListKey, list);
            await _localStorageService.SetItemAsync(LocalStorageConstants.EmotesListExpirationKey, DateTime.Now.AddMinutes(1));

            return list;
        }
        public async Task<Emote> GetEmoteDetails(int idEmote)
        {
            return await JsonSerializer.DeserializeAsync<Emote>
                (await _httpClient.GetStreamAsync($"api/Emote/{idEmote}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        public async Task UpdateEmote(Emote emote)
        {
            var emoteJson = new StringContent(JsonSerializer.Serialize(emote), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/Emote", emoteJson);
        }

        public List<Emote> ObtenerTendencias(List<Emote> emotes)
        {
            // Filtra y ordena los emotes en tendencia
            return emotes
                .Where(e => e.NumDownload >= 10)
                .OrderByDescending(e => e.NumDownload)
                .Take(5)
                .ToList();

        }
    }
}
