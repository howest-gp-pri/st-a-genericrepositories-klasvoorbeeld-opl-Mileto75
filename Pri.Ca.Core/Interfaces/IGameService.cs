using Pri.Ca.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ca.Core.Interfaces
{
    public interface IGameService
    {
        Task<GameResultModel> AddAsync(GameAddModel gameAddModel);
        Task<GameResultModel> UpdateAsync(GameUpdateModel gameUpdateModel);
        Task<GameResultModel> DeleteAsync(int id);
        Task<GameResultModel> GetAllAsync();
        Task<GameResultModel> GetByIdAsync(int id);
    }
}
