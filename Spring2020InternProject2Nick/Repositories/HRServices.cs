using Microsoft.EntityFrameworkCore;
using Spring2020InternProject2Nick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spring2020InternProject2Nick.Repositories
{
    public class HRServices : IHRServices
    {

        private ApplicationDbContext _dbContext;
        public HRServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;            
        }
    }
}
