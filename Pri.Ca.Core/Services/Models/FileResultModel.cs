using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ca.Core.Services.Models
{
    public class FileResultModel
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public string Filename { get; set; }
    }
}
