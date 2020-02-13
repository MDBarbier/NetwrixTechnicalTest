using System.Collections.Generic;
using System.Linq;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.ServiceLayer
{
    /// <summary>
    /// Business logic pertaining to the Customer Model
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

        public Customer GetCustomerById(int customerId)
        {
            return DbContext.Customers.Where(customer => customer.CustomerId == customerId).FirstOrDefault();
        }

        public Dictionary<int, CustomerViewModel> GetCustomers()
        {
            var customers = DbContext.Customers.ToList();
            var customerDataList = new Dictionary<int, CustomerViewModel>();            

            foreach (var customer in customers)
            {
                var customerToAdd = new CustomerViewModel();                                
                customerToAdd.Name = customer.Name;
                
                customerToAdd.MostRecentInvoiceRef = InvoiceAccessMethods.GetMostRecentInvoiceRef(customer.CustomerId);
                customerToAdd.MostRecentInvoiceAmount = InvoiceAccessMethods.GetMostRecentInvoiceAmount(customer.CustomerId);
                customerToAdd.NumberOfOutstandingInvoices = InvoiceAccessMethods.GetNumberOfOutstandingInvoicesForCustomer(customer.CustomerId);
                customerToAdd.TotalOfAllOutstandingInvoices = InvoiceAccessMethods.GetAmountOwedOnInvoices(customer.CustomerId);
                customerToAdd.TotalOfAllPaidInvoices = InvoiceAccessMethods.GetAmountPaidOnInvoices(customer.CustomerId);

                customerDataList.Add(customer.CustomerId, customerToAdd);
            }

            return customerDataList;
        }        
    }
}
