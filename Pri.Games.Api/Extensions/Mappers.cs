using Pri.Ca.Core.Entities;
using Pri.Ca.Core.Services.Models;
using Pri.Games.Api.DTOs;
using Pri.Games.Api.DTOs.Request.Games;
using Pri.Games.Api.DTOs.Response.Games;

namespace Pri.Games.Api.Extensions
{
    public static class Mappers
    {
        //what classes to extend to
        //extend to Dto
        public static void Map(this GamesGetDto gamesGetDto, Game game)
        {
            gamesGetDto.Id = game.Id;
            gamesGetDto.Value = game.Title;
            gamesGetDto.Description = game.Description;
            gamesGetDto.Publisher = new BaseDto
            {
                Id = game.Publisher.Id,
                Value = game.Publisher.Name
            };
            gamesGetDto.Genres = game.Genres.Select(g => new BaseDto
            {
                Id = g.Id,
                Value = g.Name
            });
        }
        public static void Map(this GamesGetAllDto gamesGetAllDto,IEnumerable<Game> games,HttpContext httpContext)
        {
            gamesGetAllDto.Games
                = games.Select(g => g.Map(httpContext)
                );
        }
        //extend to entity Game
        public static GamesGetDto Map(this Game game, HttpContext httpContext)
        {
            var imageUrl = "";
            if(!string.IsNullOrEmpty(game.Image))
            {
                imageUrl = $"{httpContext.Request.Scheme}://" +
                $"{httpContext.Request.Host.Value}/" +
                $"images/{typeof(Game).Name}/{game.Image}";
            }
            
            return new GamesGetDto
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
                ImageUrl = imageUrl
            };
        }
        public static IEnumerable<GamesGetDto> Map(this IEnumerable<Game> games,HttpContext httpContext)
        {
            return games.Select(g => g.Map(httpContext));
        }
        
        public static GameAddModel Map(this GamesCreateDto gamesCreateDto)
        {
            return new GameAddModel
            {
                Title = gamesCreateDto.Title,
                Description = gamesCreateDto.Description,
                PublisherId = gamesCreateDto.PublisherId,
                GenreIds = gamesCreateDto.Genres,
                Image = gamesCreateDto.Image,
            };
        }
        public static GameUpdateModel Map(this GamesUpdateDto gamesUpdateDto)
        {
            return new GameUpdateModel
            {
                Id = gamesUpdateDto.Id,
                Title = gamesUpdateDto.Title,
                Description = gamesUpdateDto.Description,
                GenreIds = gamesUpdateDto.Genres,
                PublisherId = gamesUpdateDto.PublisherId,
            };
        }
    }
}
