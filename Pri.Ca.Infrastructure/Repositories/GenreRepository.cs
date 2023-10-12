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
    public class GenreRepository : Baserepository<Genre>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext applicationDbContext, ILogger<Baserepository<Genre>> logger) : base(applicationDbContext, logger)
        {
            
        }

        public override async Task<Genre> GetByIdAsync(int id)
        {
            return await _table
                .Include(g => g.Games)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
