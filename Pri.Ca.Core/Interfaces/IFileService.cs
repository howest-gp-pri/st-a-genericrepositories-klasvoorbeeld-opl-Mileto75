using Microsoft.AspNetCore.Http;
using Pri.Ca.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ca.Core.Interfaces
{
    public interface IFileService
    {
        Task<FileResultModel> StoreFile<T>(IFormFile formFile, string subFolder);
    }
}
