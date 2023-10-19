using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pri.Ca.Core.Interfaces;
using Pri.Ca.Core.Services.Models;
using Pri.Games.Api.DTOs;
using Pri.Games.Api.DTOs.Request.Games;
using Pri.Games.Api.DTOs.Response.Games;

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
                    Games = result.Items.Select(g => new GamesGetDto 
                    {
                        Id = g.Id,
                        Value = g.Title,
                        Publisher = new BaseDto 
                        {
                            Id = g.Publisher.Id,
                            Value = g.Publisher.Name
                        },
                        Genres = g.Genres.Select(g => new BaseDto 
                        {
                            Id = g.Id,
                            Value = g.Name
                        }),
                        Description = g.Description
                    })
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
            var gamesGetDto = new GamesGetDto
            {
                Id = game.Id,
                Value = game.Title,
                Description = game.Description,
                Publisher = new BaseDto
                {
                    Id = game.Publisher.Id,
                    Value = game.Publisher.Name
                },
                Genres = game.Genres.Select(g => new BaseDto 
                {
                    Id = g.Id,
                    Value = g.Name
                }),
            };
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
                Games = result.Items.Select(g => new GamesGetDto
                {
                    Id = g.Id,
                    Value = g.Title,
                    Publisher = new BaseDto
                    {
                        Id = g.Publisher.Id,
                        Value = g.Publisher.Name
                    },
                    Genres = g.Genres.Select(g => new BaseDto
                    {
                        Id = g.Id,
                        Value = g.Name
                    })
                })
            };
            return Ok(gamesSearchByNameDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create(GamesCreateDto gamesCreateDto)
        {
            var gameAddmodel = new GameAddModel
            {
                Title = gamesCreateDto.Title,
                Description = gamesCreateDto.Description,
                PublisherId = gamesCreateDto.PublisherId,
                GenreIds = gamesCreateDto.Genres,
            };
            var result = await _gameService
                .AddAsync(gameAddmodel);
            if (result.IsSuccess)
            {
                return Ok("created"); 
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState.Values);
        }
        [HttpPut]
        public IActionResult Update(int id)
        {
            return Ok("update");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok("Delete");
        }
    }
}
