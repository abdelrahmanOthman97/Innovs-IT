using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innovs_IT.DAL.Database
{
    public class DbContainer : IdentityDbContext
    {
        public DbContainer(DbContextOptions<DbContainer> opts) : base(opts) { }


        
        

    }
}
