using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TechnicalTestApp.Models;
using TechnicalTestApp.ServiceLayer;
using TechnicalTestApp.ViewModels;

namespace TechnicalTestApp.Controllers
{
    /// <summary>
    /// Main controller of the application
    /// </summary>
    public class HomeController : Controller
    {        
        private readonly ICustomerAccessMethods CustomerAccessMethods;
        private readonly IInvoiceAccessMethods InvoiceAccessMethods;

        public HomeController(IInvoiceAccessMethods invoiceAccessMethods, ICustomerAccessMethods customerAccessMethods)
        {            
            CustomerAccessMethods = customerAccessMethods;
            InvoiceAccessMethods = invoiceAccessMethods;            
        }

        public IActionResult Index()
        {
            var sw = new Stopwatch();
            sw.Start();

            var homeViewModel = new HomeViewModel()
            {
                TotalPaidInvoiceCount = InvoiceAccessMethods.GetSumOfInvoicesHeld(true),
                PaidInvoiceTotal = InvoiceAccessMethods.GetTotalFundsInvoiced(),
                Customers = GetCustomers()
            };

            sw.Stop();
            homeViewModel.LoadingTime = sw.ElapsedMilliseconds;

            return View(homeViewModel);
        }        

        /// <summary>
        /// Private method which consumes data from the database via the ServiceLayer and processes it into the format required for the home page
        /// </summary>
        /// <returns>Dictionary containing the customerId as the key, and a CustomerViewModel object as the value</returns>
        private Dictionary<int, CustomerViewModel> GetCustomers()
        {
            var customers = CustomerAccessMethods.GetAllCustomers();
            var invoices = InvoiceAccessMethods.GetAllInvoices();

            var customerDataList = new Dictionary<int, CustomerViewModel>();

            Parallel.ForEach(customers, (customer) =>
            {

                var customerToAdd = new CustomerViewModel
                {
                    Name = customer.Value.Name
                };

                var customerInvoices = GetInvoicesForCustomer(customer.Key, invoices);

                var mostRecentInvoice = GetMostRecentInvoiceForCustomer(customerInvoices);
                customerToAdd.MostRecentInvoiceRef = mostRecentInvoice.InvoiceId;
                customerToAdd.MostRecentInvoiceAmount = mostRecentInvoice.Value;
                customerToAdd.NumberOfOutstandingInvoices = GetNumberOfOutstandingInvoicesForCustomer(customerInvoices);
                customerToAdd.TotalOfAllOutstandingInvoices = GetAmountOwedOnOutstandingInvoicesForCustomer(customerInvoices);
                customerToAdd.TotalOfAllPaidInvoices = GetAmountPaidOnInvoices(customerInvoices);

                lock (customerDataList)
                {
                    customerDataList.Add(customer.Key, customerToAdd);
                }

            });

            return customerDataList;
        }

        /// <summary>
        /// Get invoices for the specified customer from the supplied invoices
        /// </summary>
        /// <param name="customerId">the customer ID to look for</param>
        /// <param name="Invoices">the group of invoices to check</param>
        /// <returns>Dictionary containing the customerId as the key, and a CustomerViewModel object as the value</returns>
        private Dictionary<int, Invoice> GetInvoicesForCustomer(int customerId, Dictionary<int, Invoice> Invoices)
        {
            return Invoices.Values.Where(invoice => invoice.CustomerId == customerId).ToDictionary(invoice => invoice.InvoiceId, invoice => invoice);
        }

        private Invoice GetMostRecentInvoiceForCustomer(Dictionary<int, Invoice> Invoices)
        {
            return Invoices.Values
                .OrderByDescending(invoice => invoice.InvoiceDate)
                .FirstOrDefault();
        }

        /// <summary>
        /// Get the number of outstanding invoices for the specified customer from the supplied invoices
        /// </summary>
        /// <param name="customerId">the customer ID to look for</param>
        /// <param name="Invoices">the group of invoices to check</param>
        /// <returns>A long which indicated the number of outstanding invoices</returns>
        private long GetNumberOfOutstandingInvoicesForCustomer(Dictionary<int, Invoice> Invoices)
        {
            return Invoices.Values.Where(i => !i.IsPaid).LongCount();
        }

        /// <summary>
        /// Get the amount owed on outstanding invoices for the specified customer from the supplied invoices
        /// </summary>
        /// <param name="customerId">the customer ID to look for</param>
        /// <param name="Invoices">the group of invoices to check</param>
        /// <returns>decimal indicating the amount owed across all invoices</returns>
        private decimal GetAmountOwedOnOutstandingInvoicesForCustomer(Dictionary<int, Invoice> Invoices)
        {
            return Invoices.Values.Where(i => !i.IsPaid).Select(i => i.Value).AsEnumerable().Sum();
        }

        /// <summary>
        /// Get the total amount paid for the specified customer from the supplied invoices
        /// </summary>
        /// <param name="customerId">the customer ID to look for</param>
        /// <param name="Invoices">the group of invoices to check</param>
        /// <returns>decimal indicating the total amount paid by the customer</returns>
        private decimal GetAmountPaidOnInvoices(Dictionary<int, Invoice> Invoices)
        {
            return Invoices.Values.Where(i => i.IsPaid).Select(i => i.Value).AsEnumerable().Sum();
        }
    }
}
