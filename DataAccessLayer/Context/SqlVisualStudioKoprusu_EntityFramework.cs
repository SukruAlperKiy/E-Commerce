using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    //bu kisim sql ile visual studio arasinda kopru kuruyor. Visual Studio benim sql'e buradan baglaniyor.
    public class SqlVisualStudioKoprusu_EntityFramework:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = MSI\\SQLEXPRESS; initial catalog=eCommerce; integrated security=true; TrustServerCertificate=True;");
        }

        public DbSet<Slider> EfSliders { get; set; }

    }
}
