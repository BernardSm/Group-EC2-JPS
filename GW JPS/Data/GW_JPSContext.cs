using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GW_JPS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GW_JPS.Data
{
    public class GW_JPSContext : IdentityDbContext<ApplicationUser>
    {
        public GW_JPSContext (DbContextOptions<GW_JPSContext> options)
            : base(options)
        {
        }

        public DbSet<GW_JPS.Models.Bill> Bill { get; set; }
    }
}
