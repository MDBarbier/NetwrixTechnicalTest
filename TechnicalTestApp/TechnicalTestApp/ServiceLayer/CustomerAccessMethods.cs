using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.ServiceLayer
{
    /// <summary>
    /// Database access methods pertaining to the Customer Model
    /// </summary>
    public class CustomerAccessMethods : ICustomerAccessMethods
    {
        public IApplicationDatabaseContext DbContext { get; }
        public IInvoiceAccessMethods InvoiceAccessMethods { get; }
        
        public CustomerAccessMethods(IApplicationDatabaseContext databaseContext)
        {
            DbContext = databaseContext;
            InvoiceAccessMethods = new InvoiceAccessMethods(DbContext);
        }

        /// <summary>
        /// Get a customer with matching ID if it exists
        /// </summary>
        /// <param name="customerId">The customerId to match on</param>
        /// <returns>A customer object</returns>
        public Customer GetCustomerById(int customerId)
        {
            return DbContext.Customers.Where(customer => customer.CustomerId == customerId).FirstOrDefault();
        }

        /// <summary>
        /// Get all customer objects
        /// </summary>
        /// <returns>A dictionary, the key being the customerId and the value the customer object</returns>
        public Dictionary<int, Customer> GetAllCustomers()
        {
            return DbContext.Customers.AsNoTracking().ToDictionary(customer => customer.CustomerId, customer => customer);
        }        
    }
}
