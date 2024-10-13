using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataAccessLayer.Concrete
{

    public class CurrencyDbContext : DbContext
    {
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyRate>()
                .Property(c => c.BuyingRate)
                .HasColumnType("decimal(18, 4)"); // 18 toplam basamak, 4 ondalık basamak

            base.OnModelCreating(modelBuilder);
        }

    }


}
