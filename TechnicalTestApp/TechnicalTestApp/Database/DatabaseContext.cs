using Microsoft.EntityFrameworkCore;
using TechnicalTestApp.Models;

namespace TechnicalTestApp.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Database\\CustomerInvoiceData.db");
        }
    }
}
