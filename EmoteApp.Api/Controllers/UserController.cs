using BethanysPieShopHRM.Api.Models;
using Microsoft.AspNetCore.Mvc;


namespace BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userRepository.GetAllUsers());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok(_userRepository.GetUserById(id));
        }
    }
}
