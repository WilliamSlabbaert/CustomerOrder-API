using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class DataContext : DbContext
    {
        public String dataString;
       
        public DataContext(String type = "test")
        {
            if (!type.Equals("test"))
                dataString = @"";
            else
                dataString = @"Data Source=WILLIAM-SLABBAE\SQLEXPRESS;Initial Catalog=KlantBestellingenTest;Integrated Security=True";
        }
        public DataContext()
        {
            dataString = @"Data Source=WILLIAM-SLABBAE\SQLEXPRESS;Initial Catalog=KlantBestellingenTest;Integrated Security=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(dataString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasOne(s => s.Customer).WithMany(s => s.orderList).IsRequired();
            modelBuilder.Entity<Customer>().HasMany(s => s.orderList).WithOne(s => s.Customer).IsRequired();
        }

        public virtual DbSet<Customer> CustomerData { get; set; }
        public virtual DbSet<Order> OrderData { get; set; }
    }
}
