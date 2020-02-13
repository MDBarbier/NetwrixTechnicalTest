using System.Collections.Generic;
using System.Linq;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.ServiceLayer
{
    public class CustomerAccessMethods : ICustomerAccessMethods
    {
        public IApplicationDatabaseContext DbContext { get; }

        public CustomerAccessMethods(IApplicationDatabaseContext databaseContext)
        {
            DbContext = databaseContext;            
        }        

        public Customer GetCustomerById(int customerId)
        {
            return DbContext.Customers.Where(customer => customer.CustomerId == customerId).FirstOrDefault();
        }

        public Dictionary<int, CustomerViewModel> GetCustomers()
        {
            var customers = DbContext.Customers.ToList();
            var customerDataList = new Dictionary<int, CustomerViewModel>();
            var invoiceAccessMethods = new InvoiceAccessMethods(DbContext);

            foreach (var customer in customers)
            {
                var customerToAdd = new CustomerViewModel();                                
                customerToAdd.Name = customer.Name;
                
                customerToAdd.MostRecentInvoiceRef = invoiceAccessMethods.GetMostRecentInvoiceRef(customer.CustomerId);
                customerToAdd.MostRecentInvoiceAmount = invoiceAccessMethods.GetMostRecentInvoiceAmount(customer.CustomerId);
                customerToAdd.NumberOfOutstandingInvoices = invoiceAccessMethods.GetNumberOfOutstandingInvoicesForCustomer(customer.CustomerId);
                customerToAdd.TotalOfAllOutstandingInvoices = invoiceAccessMethods.GetAmountOwedOnInvoices(customer.CustomerId, false);
                customerToAdd.TotalOfAllPaidInvoices = invoiceAccessMethods.GetAmountOwedOnInvoices(customer.CustomerId, true);

                customerDataList.Add(customer.CustomerId, customerToAdd);
            }

            return customerDataList;
        }        
    }
}
