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
    public abstract class Baserepository<T> : 
        IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<Baserepository<T>> _logger;
        protected readonly DbSet<T> _table;

        public Baserepository(ApplicationDbContext applicationDbContext
            , ILogger<Baserepository<T>> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _table = _applicationDbContext.Set<T>();
        }

        public async Task<bool> AddAsync(T toAdd)
        {
            _table.Add(toAdd);
            return await SaveChangesasync();
        }

        public async Task<bool> DeleteAsync(T toDelete)
        {
            _table.Remove(toDelete);
            return await SaveChangesasync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _table.FirstOrDefaultAsync(
                t => t.Id == id);
        }

        private async Task<bool> SaveChangesasync()
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

        public async Task<bool> UpdateAsync(T toUpdate)
        {
            var entity = await GetByIdAsync(toUpdate.Id);
            entity = toUpdate;
            return await SaveChangesasync();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _table.AsQueryable();
        }
    }
}
