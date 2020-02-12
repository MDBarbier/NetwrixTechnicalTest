using System;
using System.Linq;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;

namespace TechnicalTestApp.ServiceLayer
{
    public class InvoiceAccessMethods
    {
        private readonly IApplicationDatabaseContext _myDbContext;

        public InvoiceAccessMethods(IApplicationDatabaseContext databaseContext)
        {
            _myDbContext = databaseContext;
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return _myDbContext.Invoices.Where(invoice => invoice.InvoiceId == invoiceId).FirstOrDefault();
        }

        public long GetSumOfInvoicesHeld(bool paidInvoicesOnly)
        {
            var invoices = paidInvoicesOnly ? _myDbContext.Invoices.Where(invoice => invoice.IsPaid).ToList() :
                                            _myDbContext.Invoices.ToList();

            long numInvoices = 0;

            foreach (var item in invoices)
            {
                numInvoices++;
            }

            return numInvoices;
        }

        internal decimal GetTotalFundsInvoiced()
        {
            return _myDbContext.Invoices.Where(invoice => invoice.IsPaid).Select(invoice => invoice.Value).AsEnumerable().Sum();
        }

        internal long GetNumberOfOutstandingInvoicesForCustomer(int customerId)
        {
            long count = 0;

            var outstandingInvoices = _myDbContext.Invoices.Where(invoice => !invoice.IsPaid)
                                                           .Where(invoice => invoice.CustomerId == customerId)
                                                           .Select(invoice => invoice.Value);

            foreach (var invoice in outstandingInvoices)
            {
                count++;
            }

            return count;
        }

        internal decimal GetAmountOwedOnInvoices(int customerId, bool paidOnly)
        {
            //Get all customer invoices
            var invoices = _myDbContext.Invoices.Where(invoice => invoice.CustomerId == customerId);

            //Return the sum of the value of all of them, or conditionally just the paid invoices
            return paidOnly ? invoices.Where(invoice => invoice.IsPaid).Select(invoice => invoice.Value).AsEnumerable().Sum() :
                              invoices.Select(invoice => invoice.Value).AsEnumerable().Sum();                                        
        }

        internal int GetMostRecentInvoiceRef(int customerId)
        {
            return _myDbContext.Invoices
                .Where(invoice => invoice.CustomerId == customerId)
                .OrderByDescending(invoice => invoice.InvoiceDate)
                .Select(invoice => invoice.InvoiceId)
                .FirstOrDefault();
        }

        internal decimal GetMostRecentInvoiceAmount(int customerId)
        {
            return _myDbContext.Invoices
                .Where(invoice => invoice.CustomerId == customerId)
                .OrderBy(invoice => invoice.InvoiceDate)
                .Select(invoice => invoice.Value)
                .FirstOrDefault();
        }
     
    }
}
