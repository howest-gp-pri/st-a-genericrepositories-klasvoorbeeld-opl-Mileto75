using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pri.Ca.Core.Entities;
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
                Games = result.Items.Select(g => new BaseViewModel 
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
                Id = result.Items.First().Id,
                Value = result.Items.First().Title,
                Description = result.Items.First().Description,
                Publisher = new BaseViewModel 
                {
                    Id = result.Items.First().Publisher.Id,
                    Value = result.Items.First().Publisher.Name,
                },
                Genres = result.Items.First().Genres.Select(g => new BaseViewModel
                {
                    Id = g.Id,
                    Value = g.Name
                })
            };
            return View(gamesInfoViewModel);
        }
    }
}
