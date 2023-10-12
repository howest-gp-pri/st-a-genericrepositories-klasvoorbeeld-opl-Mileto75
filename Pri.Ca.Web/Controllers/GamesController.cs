using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pri.Ca.Core.Interfaces;
using Pri.Ca.Core.Interfaces.Repositories;
using Pri.Ca.Infrastructure.Data;
using Pri.Ca.Web.ViewModels;

namespace Pri.Ca.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _gameService.GetAllAsync();
            
            var gamesIndexViewModel = new GamesIndexViewModel
            {
                Games = result.Games.Select(g => new BaseViewModel 
                    {
                        Id = g.Id,
                        Value = g.Title
                    })
            };            
            return View(gamesIndexViewModel);
        }
        public async Task<IActionResult> Info(int id)
        {
            var result = await _gameService.GetByIdAsync(id);
            if(result == null)
            {
                return NotFound(result.Errors);
            }
            var gamesInfoViewModel = new GamesInfoViewModel 
            {
                Id = result.Games.First().Id,
                Value = result.Games.First().Title,
                Description = result.Games.First().Description,
                Publisher = new BaseViewModel 
                {
                    Id = result.Games.First().Publisher.Id,
                    Value = result.Games.First().Publisher.Name,
                },
                Genres = result.Games.First().Genres.Select(g => new BaseViewModel
                {
                    Id = g.Id,
                    Value = g.Name
                })
            };
            return View(gamesInfoViewModel);
        }
    }
}
