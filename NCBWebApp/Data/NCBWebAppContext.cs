using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NCBWebApp.Models;

namespace NCBWebApp.Data
{
    public class NCBWebAppContext : IdentityDbContext<ApplicationUser>
    {
        
        public NCBWebAppContext (DbContextOptions<NCBWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<NCBWebApp.Models.AccountTransaction> Transaction { get; set; }
    }
}
