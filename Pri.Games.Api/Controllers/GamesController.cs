using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pri.Games.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Game");
        }
        [HttpPost]
        public IActionResult Create()
        {
            return Ok("Created");
        }
    }
}
