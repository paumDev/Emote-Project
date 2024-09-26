using BethanysPieShopHRM.Api.Models;
using ClassLibraryEmotes;

namespace EmoteApp.Api.Models
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _context;

        public FeedbackRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Feedback> GetFeedbackForEmote(int emoteId)
        {
            return _context.Feedback.Where(f => f.EmoteId == emoteId).ToList();
        }

        public Feedback GetFeedbackById(int feedbackId)
        {
            return _context.Feedback.FirstOrDefault(f => f.FeedbackId == feedbackId);
        }

        public Feedback AddFeedback(Feedback feedback)
        {
            _context.Feedback.Add(feedback);
            _context.SaveChanges();
            return feedback;
        }

        public void UpdateFeedback(Feedback feedback)
        {
            _context.Feedback.Update(feedback);
            _context.SaveChanges();
        }

        public void DeleteFeedback(int feedbackId)
        {
            var feedback = _context.Feedback.FirstOrDefault(f => f.FeedbackId == feedbackId);
            if (feedback != null)
            {
                _context.Feedback.Remove(feedback);
                _context.SaveChanges();
            }
        }
    }

}
