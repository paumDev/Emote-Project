using ClassLibraryEmotes;
using Microsoft.AspNetCore.Mvc;
using EmoteApp.Api.Models;

namespace EmoteApp.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeedbackController(IFeedbackRepository feedbackRepository, IHttpContextAccessor httpContextAccessor)
        {
            _feedbackRepository = feedbackRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // Obtener todos los comentarios para un emote específico
        [HttpGet("emote/{emoteId}")]
        public IActionResult GetFeedbackForEmote(int emoteId)
        {
            var feedbacks = _feedbackRepository.GetFeedbackForEmote(emoteId);
            return Ok(feedbacks);
        }

        // Obtener detalles de un comentario específico
        [HttpGet("{id}")]
        public IActionResult GetFeedbackById(int id)
        {
            var feedback = _feedbackRepository.GetFeedbackById(id);

            if (feedback == null)
                return NotFound();

            return Ok(feedback);
        }

        // Crear un nuevo comentario
        [HttpPost]
        public IActionResult CreateFeedback([FromBody] Feedback feedback)
        {
            if (feedback == null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(feedback.Content))
            {
                ModelState.AddModelError("Content", "The content shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdFeedback = _feedbackRepository.AddFeedback(feedback);

            return Created("feedback", createdFeedback);
        }

        // Actualizar un comentario existente
        [HttpPut]
        public IActionResult UpdateFeedback([FromBody] Feedback feedback)
        {
            if (feedback == null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(feedback.Content))
            {
                ModelState.AddModelError("Content", "The content shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var feedbackToUpdate = _feedbackRepository.GetFeedbackById(feedback.FeedbackId);

            if (feedbackToUpdate == null)
                return NotFound();

            _feedbackRepository.UpdateFeedback(feedback);

            return NoContent(); // Success
        }

        // Votar en un comentario (positivo o negativo)
        [HttpPost("vote/{id}")]
        public IActionResult VoteFeedback(int id, [FromBody] VoteDto vote)
        {
            var feedback = _feedbackRepository.GetFeedbackById(id);

            if (feedback == null)
                return NotFound();

            if (vote.IsUpvote)
            {
                feedback.Upvotes++;
            }
            else
            {
                feedback.Downvotes++;
            }

            _feedbackRepository.UpdateFeedback(feedback);

            return Ok(feedback);
        }

        // Eliminar un comentario
        [HttpDelete("{id}")]
        public IActionResult DeleteFeedback(int id)
        {
            if (id == 0)
                return BadRequest();

            var feedbackToDelete = _feedbackRepository.GetFeedbackById(id);
            if (feedbackToDelete == null)
                return NotFound();

            _feedbackRepository.DeleteFeedback(id);

            return NoContent(); // Success
        }
    }

}
