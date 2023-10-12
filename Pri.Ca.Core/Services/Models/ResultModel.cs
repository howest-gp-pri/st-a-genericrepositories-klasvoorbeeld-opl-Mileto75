using Pri.Ca.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ca.Core.Services.Models
{
    public class ResultModel<T>
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<T> Items { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
