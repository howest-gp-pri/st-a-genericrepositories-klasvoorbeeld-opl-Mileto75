using Microsoft.EntityFrameworkCore;
using Pri.Ca.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ca.Infrastructure.Data.Seeding
{
    public static class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var publishers = new Publisher[]
                {
                    new Publisher {Id = 1,Name = "Square Enix"},
                    new Publisher {Id = 2,Name = "EA"},
                };
            var games = new Game[] 
            {
                new Game { Id = 1,Title="Final Fantasy", PublisherId=1, Description="Rpg classic"},
                new Game { Id = 2,Title="Fifa20",PublisherId=2,Description="Cool soccer game"},
            };
            var genres = new Genre[]
            {
                new Genre{Id = 1, Name = "Rpg" },
                new Genre{Id = 2, Name = "Sports" },
                new Genre{Id = 3, Name = "Fantasy" },
                new Genre{Id = 4, Name = "Adventure" },
            };
            //many to many
            var gamesGenres = new[]
            {
                new {GamesId = 1, GenresId = 1 },
                new {GamesId = 1, GenresId = 3 },
                new {GamesId = 1, GenresId = 4 },
                new {GamesId = 2, GenresId = 2 },
                new {GamesId = 2, GenresId = 3 },
            };
            //modelbuilder
            modelBuilder.Entity<Publisher>().HasData(publishers);   
            modelBuilder.Entity<Game>().HasData(games);   
            modelBuilder.Entity<Genre>().HasData(genres);   
            modelBuilder.Entity($"{nameof(Game)}{nameof(Genre)}").HasData(gamesGenres);   
        }
    }
}
