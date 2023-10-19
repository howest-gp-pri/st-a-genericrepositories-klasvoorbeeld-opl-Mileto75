using Pri.Ca.Core.Entities;
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
        Task<ResultModel<Game>> AddAsync(GameAddModel gameAddModel);
        Task<ResultModel<Game>> UpdateAsync(GameUpdateModel gameUpdateModel);
        Task<ResultModel<Game>> DeleteAsync(int id);
        Task<ResultModel<Game>> GetAllAsync();
        Task<ResultModel<Game>> GetByIdAsync(int id);
        Task<ResultModel<Game>> SearchByName(string name);
        Task<bool> IfExists(int id);
    }
}
