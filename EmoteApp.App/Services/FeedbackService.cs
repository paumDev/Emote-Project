using ClassLibraryEmotes;
using System.Text.Json;
using System.Text;

namespace EmoteApp.App.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly HttpClient _httpClient;

        public FeedbackService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener todos los comentarios y votaciones para un emote
        public async Task<IEnumerable<Feedback>> GetFeedback(int emoteId)
        {
            var list = await JsonSerializer.DeserializeAsync<IEnumerable<Feedback>>
                (await _httpClient.GetStreamAsync($"api/Feedback/emote/{emoteId}"),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return list;
        }

        // Agregar un nuevo comentario
        public async Task AddFeedback(Feedback feedback)
        {
            var feedbackJson = new StringContent(JsonSerializer.Serialize(feedback), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("api/Feedback", feedbackJson);
        }

        // Votar positivamente o negativamente en un comentario
        public async Task Vote(int feedbackId, bool isUpvote)
        {
            var voteRequest = new { IsUpvote = isUpvote };
            var voteJson = new StringContent(JsonSerializer.Serialize(voteRequest), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"api/Feedback/vote/{feedbackId}", voteJson);
        }

        // Obtener el promedio de valoraciones (si lo implementas)
        public async Task<double> GetAverageRating(int emoteId)
        {
            return await JsonSerializer.DeserializeAsync<double>
                (await _httpClient.GetStreamAsync($"api/Feedback/average/{emoteId}"),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task DeleteFeedback(int feedbackId)
        {
            await _httpClient.DeleteAsync($"api/Feedback/{feedbackId}");
        }
    }
}
