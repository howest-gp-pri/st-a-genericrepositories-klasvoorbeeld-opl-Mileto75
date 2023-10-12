using Pri.Ca.Core.Entities;
using Pri.Ca.Core.Interfaces;
using Pri.Ca.Core.Interfaces.Repositories;
using Pri.Ca.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ca.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<GameResultModel> AddAsync(GameAddModel gameAddModel)
        {
            //this is missing extra checks on
            //foreign key data Publisher and Genre
            //due to lack of repositories
            var game = new Game
            {
                Title = gameAddModel.Title,
                Description = gameAddModel.Description,
                PublisherId = gameAddModel.PublisherId,
                //genres later
            };
            if(!await _gameRepository.AddAsync(game))
            {
                return CreateErrorModel("Something went wrong...");
            }
            return CreateResultModel(null);
        }

        public async Task<GameResultModel> DeleteAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if(game == null)
            {
                return CreateErrorModel("Game not found.");
            }
            if(!await _gameRepository.DeleteAsync(game))
            {
                return CreateErrorModel("Something went wrong...Please try again later");
            }
            return CreateResultModel(null);
        }

        public async Task<GameResultModel> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return CreateResultModel(games);
        }

        public async Task<GameResultModel> GetByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if(game == null)
            {
                return CreateErrorModel("Game not found");
            }
            return CreateResultModel(new List<Game> { game });
        }

        public async Task<GameResultModel> UpdateAsync(GameUpdateModel gameUpdateModel)
        {
            //this is missing extra checks on
            //foreign key data Publisher and Genre
            //due to lack of repositories
            var game = await _gameRepository.GetByIdAsync(gameUpdateModel.Id);
            if( game == null)
            {
                return CreateErrorModel("Game not found.");
            }
            game.Title = gameUpdateModel.Title;
            game.Description = gameUpdateModel.Description;
            game.PublisherId = gameUpdateModel.PublisherId;
            //genres will be updated in a later version
            if(!await _gameRepository.UpdateAsync(game))
            {
                return CreateErrorModel("Unknown error.");
            }
            return CreateResultModel(null);
        }
        private GameResultModel CreateErrorModel(string error)
        {
            return new GameResultModel
            {
                IsSuccess = false,
                Errors = new List<string> { error }
            };
        }
        private GameResultModel CreateResultModel(IEnumerable<Game> games)
        {
            return new GameResultModel
            {
                IsSuccess = true,
                Games = games
            };
        }
    }
}
