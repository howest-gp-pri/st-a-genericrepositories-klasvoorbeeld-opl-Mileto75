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
    public class PublisherRepository : Baserepository<Publisher>, IPublisherRepository
    {
        //implementation without IPublisherRepository interface
        public PublisherRepository(ApplicationDbContext applicationDbContext, ILogger<Baserepository<Publisher>> logger) : base(applicationDbContext, logger)
        {
        }
    }
}
