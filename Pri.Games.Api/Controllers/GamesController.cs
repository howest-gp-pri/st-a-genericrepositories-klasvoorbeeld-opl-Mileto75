using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.Ca.Core.Interfaces;
using Pri.Ca.Core.Services.Models;
using Pri.Games.Api.DTOs;
using Pri.Games.Api.DTOs.Request.Games;
using Pri.Games.Api.DTOs.Response.Games;
using Pri.Games.Api.Extensions;

namespace Pri.Games.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        //declare service class
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _gameService.GetAllAsync();
            if(result.IsSuccess)
            {
                var gamesGetAllDto = new GamesGetAllDto
                {
                    Games = result.Items.Map(HttpContext)
                };
                return Ok(gamesGetAllDto);
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) 
        {
            var result = await _gameService.GetByIdAsync(id);
            if(!result.IsSuccess)
            {
                return NotFound(result.Errors.First());
            }
            var game = result.Items.First();
            var gamesGetDto = game.Map(HttpContext);
            return Ok(gamesGetDto);
        }
        [HttpGet("search/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            var result = await _gameService.SearchByName(name);
            if(!result.IsSuccess)
            {
                return NotFound(result.Errors.First());
            }
            var gamesSearchByNameDto = new GamesSearchByNameDto
            {
                Games = result.Items.Map(HttpContext)
            };
            return Ok(gamesSearchByNameDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]GamesCreateDto gamesCreateDto)
        {
            var result = await _gameService.AddAsync(gamesCreateDto.Map());
            if (result.IsSuccess)
            {
                return Ok();
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState.Values);
        }
        [HttpPut]
        public async Task<IActionResult> Update(GamesUpdateDto gamesUpdateDto)
        {
            var result = await _gameService.UpdateAsync(gamesUpdateDto.Map());
            if (result.IsSuccess)
            {
                return Ok("update");
            }
            //need to refactor this to allow a badrequest return
            //next week
            return NotFound(result.Errors);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _gameService.DeleteAsync(id);
            if(result.IsSuccess)
            {
                return Ok("Delete");
            }
            return NotFound(result.Errors);
        }
    }
}
