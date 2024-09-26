using ClassLibraryEmotes;

namespace EmoteApp.App.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetFeedback(int emoteId);
        Task AddFeedback(Feedback feedback);
        Task DeleteFeedback(int feedbackId);

        Task Vote(int feedbackId, bool isUpvote); // isUpvote = true para voto positivo
        Task<double> GetAverageRating(int emoteId);
    }
}
