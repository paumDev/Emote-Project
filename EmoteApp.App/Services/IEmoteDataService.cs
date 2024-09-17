using ClassLibraryEmotes;

namespace EmoteApp.App.Services
{
    public interface IEmoteDataService
    {
        Task<IEnumerable<Emote>> GetAllEmotes(bool refreshRequired = false);
        Task<Emote> GetEmoteDetails(int idEmote);
        Task<Emote> AddEmote(Emote emote);
        Task UpdateEmote(Emote emote);
        Task DeleteEmote(int idEmote);
        List<Emote> ObtenerTendencias(List<Emote> emotes);
    }
}
