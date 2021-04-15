using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BNS.Models;
using BNS.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BNS.Models;

namespace BNS.Data
{
    public class BNSContext : IdentityDbContext<AppUser>
    {
        public BNSContext (DbContextOptions<BNSContext> options)
            : base(options)
        {
        }

        public DbSet<BNS.Models.Customer> Customer { get; set; }

        public DbSet<BNS.Models.Teller> Teller { get; set; }

        public DbSet<BNS.Models.Transaction> Transaction { get; set; }
    }
}
