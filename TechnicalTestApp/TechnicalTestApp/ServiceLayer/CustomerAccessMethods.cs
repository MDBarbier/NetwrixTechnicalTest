using System.Collections.Generic;
using System.Linq;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.ServiceLayer
{
    public class CustomerAccessMethods
    {
        private readonly IApplicationDatabaseContext _myDbContext;

        public CustomerAccessMethods(IApplicationDatabaseContext databaseContext)
        {
            _myDbContext = databaseContext;
        }

        public Customer GetCustomerById(int customerId)
        {
            return _myDbContext.Customers.Where(customer => customer.CustomerId == customerId).FirstOrDefault();
        }

        public Dictionary<int, CustomerViewModel> GetCustomers()
        {
            var customers = _myDbContext.Customers.ToList();
            var customerDataList = new Dictionary<int, CustomerViewModel>();
            var invoiceAccessMethods = new InvoiceAccessMethods(_myDbContext);

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
