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
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<GameRepository> _logger;

        public GameRepository(ApplicationDbContext applicationDbContext, ILogger<GameRepository> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public async Task<bool> AddAsync(Game toAdd)
        {
            _applicationDbContext.Add(toAdd);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Game toDelete)
        {
            _applicationDbContext.Remove(toDelete);
            return await SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _applicationDbContext
                .Games.ToListAsync();
        }

        public async Task<Game> GetByIdAsync(int id)
        {
            return await _applicationDbContext
                .Games
                .Include(p => p.Publisher)
                .Include(p => p.Genres)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> UpdateAsync(Game toUpdate)
        {
            var game = await _applicationDbContext.Games
                .FirstOrDefaultAsync(g => g.Id == toUpdate.Id);
            game = toUpdate;
            return await SaveChangesAsync();
        }
        private async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogError(dbUpdateException.Message);
                return false;
            }
        }
    }
}
