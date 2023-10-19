using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pri.Ca.Core.Entities;
using Pri.Ca.Core.Interfaces.Repositories;
using Pri.Ca.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ca.Infrastructure.Repositories
{
    public class GameRepository : Baserepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext applicationDbContext, ILogger<Baserepository<Game>> logger) : base(applicationDbContext, logger)
        {
        }

        public override IQueryable<Game> GetAll()
        {
            return _table
                .Include(g => g.Publisher)
                .Include(g => g.Genres)
                .AsQueryable();
        }

        public override async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _table
                .Include(g => g.Publisher)
                .Include(g => g.Genres).ToListAsync();
        }

        public override async Task<Game> GetByIdAsync(int id)
        {
            return await _table
                .Include(g => g.Publisher)
                .Include(g => g.Genres).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> SearchByName(string name)
        {
            var games = GetAll();
            return await games.Where(s => s.Title
            .ToUpper()
            .Contains(name.ToUpper())).ToListAsync();
        }
    }
}
