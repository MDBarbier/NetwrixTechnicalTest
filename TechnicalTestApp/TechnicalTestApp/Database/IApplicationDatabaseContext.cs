using Microsoft.EntityFrameworkCore;
using TechnicalTestApp.Models;

namespace TechnicalTestApp.Database
{
    public interface IApplicationDatabaseContext : IDatabaseContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
