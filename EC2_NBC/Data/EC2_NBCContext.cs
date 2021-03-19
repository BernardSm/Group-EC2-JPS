using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EC2_NBC.Models;

namespace EC2_NBC.Data
{
    public class EC2_NBCContext : DbContext
    {
        public EC2_NBCContext (DbContextOptions<EC2_NBCContext> options)
            : base(options)
        {
        }

        public DbSet<EC2_NBC.Models.Customer> Customer { get; set; }
    }
}
