using Microsoft.EntityFrameworkCore;
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
        private readonly IGenreRepository _genreRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IFileService _fileService;

        public GameService(IGameRepository gameRepository, IGenreRepository genreRepository, IPublisherRepository publisherRepository, IFileService fileService)
        {
            _gameRepository = gameRepository;
            _genreRepository = genreRepository;
            _publisherRepository = publisherRepository;
            _fileService = fileService;
        }

        public async Task<ResultModel<Game>> AddAsync(GameAddModel gameAddModel)
        {
            var publisher = await _publisherRepository.GetByIdAsync(gameAddModel.PublisherId);
            if(publisher == null)
            {
                return CreateErrorModel("Unknown publisher");
            }
            var genresCount = _genreRepository.GetAll()
                .Where(g => gameAddModel.GenreIds.Contains(g.Id)).Count();
            if (genresCount
                != gameAddModel.GenreIds.Count())
            {
                return CreateErrorModel("Unknown genres");
            }
            if(genresCount == 0 && gameAddModel.GenreIds.Count() == 0)
            {
                return CreateErrorModel("Please provide a genre!");
            }
            
            if(_gameRepository.GetAll().Any(g => g.Title.ToUpper().Equals(gameAddModel.Title.ToUpper())))
            {
                return CreateErrorModel("Name exists");
            }
            //store the file
            var fileResult = await _fileService.StoreFile<Game>(gameAddModel.Image, "images");
            if(!fileResult.IsSuccess)
            {
                return CreateErrorModel(fileResult.Error);
            }
            var game = new Game
            {
                Title = gameAddModel.Title,
                Description = gameAddModel.Description,
                PublisherId = gameAddModel.PublisherId,
                Image = fileResult.Filename
                //genres later
            };
            if(!await _gameRepository.AddAsync(game))
            {
                return CreateErrorModel("Something went wrong...");
            }
            return CreateResultModel(null);
        }

        public async Task<ResultModel<Game>> DeleteAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
            {
                return CreateErrorModel("Game not found.");
            }
            if (!await _gameRepository.DeleteAsync(game))
            {
                return CreateErrorModel("Something went wrong...Please try again later");
            }
            return CreateResultModel(null);
        }

        public async Task<ResultModel<Game>> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return CreateResultModel(games);
        }

        public async Task<ResultModel<Game>> GetByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if(game == null)
            {
                return CreateErrorModel("Game not found");
            }
            return CreateResultModel(new List<Game> { game });
        }

        public async Task<ResultModel<Game>> SearchByName(string name)
        {
            //get the games
            //var queryObject = _gameRepository.GetAll().
            //    Where(g => g.Title.ToUpper().Equals(name.ToUpper()));
            //var games = await queryObject.ToListAsync();
            //return CreateResultModel(games);
            var games = await _gameRepository.SearchByName(name);
            if(games.Count() == 0)
            {
                return CreateErrorModel("No games found!");
            }
            return CreateResultModel(games);
        }

        public async Task<ResultModel<Game>> UpdateAsync(GameUpdateModel gameUpdateModel)
        {
            //this is missing extra checks on
            //foreign key data Publisher and Genre
            //due to lack of repositories
            var game = await _gameRepository.GetByIdAsync(gameUpdateModel.Id);
            if( game == null)
            {
                return CreateErrorModel("Game not found.");
            }
            var publisher = await _publisherRepository.GetByIdAsync(gameUpdateModel.PublisherId);
            if (publisher == null)
            {
                return CreateErrorModel("Unknown publisher");
            }
            var genresCount = _genreRepository.GetAll()
                .Where(g => gameUpdateModel.GenreIds.Contains(g.Id)).Count();
            if (genresCount
                != gameUpdateModel.GenreIds.Count())
            {
                return CreateErrorModel("Unknown genres");
            }
            if (genresCount == 0 && gameUpdateModel.GenreIds.Count() == 0)
            {
                return CreateErrorModel("Please provide a genre!");
            }
            //check for title only if title update
            if(game.Title != gameUpdateModel.Title)
            {
                if (_gameRepository.GetAll().Any(g => g.Title.ToUpper().Equals(gameUpdateModel.Title.ToUpper())))
                {
                    return CreateErrorModel("Name exists");
                }
            }
            game.Title = gameUpdateModel.Title;
            game.Description = gameUpdateModel.Description;
            game.PublisherId = gameUpdateModel.PublisherId;
            game.Genres = await _genreRepository
                .GetAll().Where(g => gameUpdateModel.GenreIds.Contains(g.Id)).ToListAsync();
            if (!await _gameRepository.UpdateAsync(game))
            {
                return CreateErrorModel("Unknown error.");
            }
            return CreateResultModel(null);
        }
        public async Task<bool> IfExists(int id)
        {
            return await _gameRepository.GetAll().AnyAsync(g => g.Id == id);
        }
        private ResultModel<Game> CreateErrorModel(string error)
        {
            return new ResultModel<Game>
            {
                IsSuccess = false,
                Errors = new List<string> { error }
            };
        }
        private ResultModel<Game> CreateResultModel(IEnumerable<Game> games)
        {
            return new ResultModel<Game>
            {
                IsSuccess = true,
                Items = games
            };
        }
    }
}
