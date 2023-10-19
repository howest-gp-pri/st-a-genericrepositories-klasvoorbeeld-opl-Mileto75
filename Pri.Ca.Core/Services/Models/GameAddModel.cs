using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.Ca.Core.Services.Models
{
    public class GameAddModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int PublisherId { get; set; }
        public IEnumerable<int> GenreIds { get; set; }
        public IFormFile Image { get; set; }
    }
}
