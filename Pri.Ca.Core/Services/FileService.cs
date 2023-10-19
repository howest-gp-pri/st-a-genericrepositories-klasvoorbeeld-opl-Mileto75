using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pri.Ca.Core.Interfaces;
using Pri.Ca.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ca.Core.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<FileResultModel> StoreFile<T>(IFormFile formFile, string subFolder)
        {
            //create a unique filename
            var filename = $"{Guid.NewGuid()}_{formFile.FileName}";
            //create the path to folder wwwroot
            var pathToFolder = Path.Combine(_webHostEnvironment.WebRootPath, subFolder, typeof(T).Name);
            //check if path exists if not create
            if(!Directory.Exists(pathToFolder) )
            {
                try 
                {
                    Directory.CreateDirectory(pathToFolder);
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    return new FileResultModel 
                    { 
                        IsSuccess = false, Error = "File not stored. try again later."
                    };
                }
                
            }
            //create full path to file
            var fullPathToFile = Path.Combine(pathToFolder, filename);
            //store the file
            using(FileStream fileStream = new FileStream(fullPathToFile,FileMode.Create))
            {
                //copy from iformfile to disk
                try 
                {
                    await formFile.CopyToAsync(fileStream);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    return new FileResultModel
                    {
                        IsSuccess = false,
                        Error = "File not stored. try again later."
                    };
                }
            }
            //return the filename
            return new FileResultModel
            {
                IsSuccess = true,
               Filename = filename
            };
        }
    }
}
