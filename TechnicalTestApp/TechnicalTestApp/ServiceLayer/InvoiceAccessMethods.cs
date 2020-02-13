using System.Linq;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;

namespace TechnicalTestApp.ServiceLayer
{
    /// <summary>
    /// Business logic pertaining to the Invoice Model
    /// </summary>
    public class InvoiceAccessMethods : IInvoiceAccessMethods
    {
        public IApplicationDatabaseContext DbContext { get; }

        public InvoiceAccessMethods(IApplicationDatabaseContext databaseContext)
        {
            DbContext = databaseContext;
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return DbContext.Invoices.Where(invoice => invoice.InvoiceId == invoiceId).FirstOrDefault();
        }

        public long GetSumOfInvoicesHeld(bool paidInvoicesOnly)
        {
            var invoices = paidInvoicesOnly ? DbContext.Invoices.Where(invoice => invoice.IsPaid).ToList() :
                                            DbContext.Invoices.ToList();

            long numInvoices = 0;

            foreach (var item in invoices)
            {
                numInvoices++;
            }

            return numInvoices;
        }

        public decimal GetTotalFundsInvoiced()
        {
            return DbContext.Invoices.Where(invoice => invoice.IsPaid).Select(invoice => invoice.Value).AsEnumerable().Sum();
        }

        public long GetNumberOfOutstandingInvoicesForCustomer(int customerId)
        {
            long count = 0;

            var outstandingInvoices = DbContext.Invoices.Where(invoice => !invoice.IsPaid)
                                                           .Where(invoice => invoice.CustomerId == customerId)
                                                           .Select(invoice => invoice.Value);

            foreach (var invoice in outstandingInvoices)
            {
                count++;
            }

            return count;
        }

        public decimal GetAmountOwedOnInvoices(int customerId)
        {
            //Get all customer invoices
            var invoices = DbContext.Invoices.Where(invoice => invoice.CustomerId == customerId);

            //Return the sum of the value of all of them, or conditionally just the paid invoices
            return invoices.Where(invoice => !invoice.IsPaid).Select(invoice => invoice.Value).AsEnumerable().Sum();                   
        }

        public decimal GetAmountPaidOnInvoices(int customerId)
        {
            //Get all customer invoices
            var invoices = DbContext.Invoices.Where(invoice => invoice.CustomerId == customerId);

            //Return the sum of the value of all of them, or conditionally just the paid invoices
            return invoices.Where(invoice => invoice.IsPaid).Select(invoice => invoice.Value).AsEnumerable().Sum();
        }

        public int GetMostRecentInvoiceRef(int customerId)
        {
            return DbContext.Invoices
                .Where(invoice => invoice.CustomerId == customerId)
                .OrderByDescending(invoice => invoice.InvoiceDate)
                .Select(invoice => invoice.InvoiceId)
                .FirstOrDefault();
        }

        public decimal GetMostRecentInvoiceAmount(int customerId)
        {
            return DbContext.Invoices
                .Where(invoice => invoice.CustomerId == customerId)
                .OrderByDescending(invoice => invoice.InvoiceDate)
                .Select(invoice => invoice.Value)
                .FirstOrDefault();
        }
     
    }
}
