using BethanysPieShopHRM.Api.Models;
using ClassLibraryEmotes;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmoteController : Controller
    {
        private readonly IEmoteRepository _emoteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmoteController(IEmoteRepository emoteRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _emoteRepository = emoteRepository;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult GetAllEmotes()
        {
            return Ok(_emoteRepository.GetAllEmotes());
        }

        [HttpGet("{id}")]
        public IActionResult GetEmoteById(int id)
        {
            return Ok(_emoteRepository.GetEmoteById(id));
        }

        [HttpPost]
        public IActionResult CreateEmote([FromBody] Emote emote)
        {
            if (emote == null)
                return BadRequest();

            if (emote.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //handle image upload
            string currentUrl = _httpContextAccessor.HttpContext.Request.Host.Value;
            var path = $"{_webHostEnvironment.WebRootPath}\\uploads\\{emote.ImageName}";
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(emote.ImageEmoteContent, 0, emote.ImageEmoteContent.Length);
            fileStream.Close();

            emote.ImageName = $"https://{currentUrl}/uploads/{emote.ImageName}";

            var createdEmote = _emoteRepository.AddEmote(emote);

            return Created("emote", createdEmote);
        }

        [HttpPut]
        public IActionResult UpdateEmote([FromBody] Emote emote)
        {
            if (emote == null)
                return BadRequest();

            if (emote.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var emoteToUpdate = _emoteRepository.GetEmoteById(emote.EmoteId);

            if (emoteToUpdate == null)
                return NotFound();

            _emoteRepository.UpdateEmote(emote);

            return NoContent(); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmote(int id)
        {
            if (id == 0)
                return BadRequest();

            var employeeToDelete = _emoteRepository.GetEmoteById(id);
            if (employeeToDelete == null)
                return NotFound();

            _emoteRepository.DeleteEmote(id);

            return NoContent();//success
        }

        [HttpGet("{emoteId}/history")]
        public async Task<ActionResult<IEnumerable<EmoteChangeLog>>> GetEmoteHistory(int emoteId)
        {
            var logs = await _emoteRepository.GetChangeLogsByEmoteIdAsync(emoteId);
            if (logs == null)
            {
                return NotFound();
            }
            return Ok(logs);
        }

        [HttpPost("history")]
        public async Task<ActionResult> AddEmoteChangeLog(EmoteChangeLog log)
        {
            await _emoteRepository.AddChangeLogAsync(log);
            return NoContent();
        }
    }
}
