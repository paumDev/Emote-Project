using ClassLibraryEmotes;

namespace EmoteApp.Api.Models
{
    public interface IFeedbackRepository
    {
        IEnumerable<Feedback> GetFeedbackForEmote(int emoteId);   // Obtener comentarios de un emote
        Feedback GetFeedbackById(int feedbackId);                  // Obtener un comentario por su ID
        Feedback AddFeedback(Feedback feedback);                   // Agregar un nuevo comentario
        void UpdateFeedback(Feedback feedback);                    // Actualizar un comentario
        void DeleteFeedback(int feedbackId);                       // Eliminar un comentario
    }

}
