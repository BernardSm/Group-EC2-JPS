using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NCBWebApp.Models;

namespace NCBWebApp.Data
{
    public class NCBWebAppContext : DbContext
    {
        
        public NCBWebAppContext (DbContextOptions<NCBWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<NCBWebApp.Models.Customer> Customer { get; set; }

        public DbSet<NCBWebApp.Models.Teller> Teller { get; set; }

        public DbSet<NCBWebApp.Models.AccountTransaction> Transaction { get; set; }
    }
}
