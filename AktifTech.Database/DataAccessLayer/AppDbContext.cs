using AktifTech.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Database.DataAccessLayer
{
    public class AppDbContext  : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<CustomerOrder> CustomerOrder { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Initilazier.Build();
            var test = Initilazier.Configuration.GetConnectionString("SqlCon");
#if DEBUG
            optionsBuilder.UseSqlServer(Initilazier.Configuration.GetConnectionString("SqlCon"));
            optionsBuilder.LogTo(s => System.Diagnostics.Debug.WriteLine(s)); 
#else
            optionsBuilder.UseSqlServer(Initilazier.Configuration.GetConnectionString("SqlCon"));
#endif
        }
    }
}
