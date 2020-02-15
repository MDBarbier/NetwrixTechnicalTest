using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TechnicalTestApp.Database;
using TechnicalTestApp.Models;

namespace TechnicalTestApp.ServiceLayer
{
    /// <summary>
    /// Database access methods pertaining to the Invoice Model
    /// </summary>
    public class InvoiceAccessMethods : IInvoiceAccessMethods
    {
        public IApplicationDatabaseContext DbContext { get; }

        public InvoiceAccessMethods(IApplicationDatabaseContext databaseContext)
        {
            DbContext = databaseContext;
        }

        /// <summary>
        /// Get the invoice which has the specified invoiceId, if it exists
        /// </summary>
        /// <param name="invoiceId">the invoiceId to find</param>
        /// <returns>An invoice object</returns>
        public Invoice GetInvoiceById(int invoiceId)
        {
            return DbContext.Invoices.Where(invoice => invoice.InvoiceId == invoiceId).FirstOrDefault();
        }

        /// <summary>
        /// Get all invoices
        /// </summary>
        /// <param name="invoiceId">the invoiceId to find</param>
        /// <returns>A Dictionary, the key being the invoiceId and the value the Invoice object</returns>
        public Dictionary<int, Invoice> GetAllInvoices()
        {
            return DbContext.Invoices.AsNoTracking().ToDictionary(invoice => invoice.InvoiceId, invoice => invoice);
        }

        /// <summary>
        /// Get the total number of invoices held
        /// </summary>
        /// <param name="paidInvoicesOnly">If true only count paid invoices</param>
        /// <returns>long confirming the number of invoices</returns>
        public long GetSumOfInvoicesHeld(bool paidInvoicesOnly)
        {
            return 
                paidInvoicesOnly ? 
                DbContext.Invoices.Where(invoice => invoice.IsPaid).LongCount() :
                DbContext.Invoices.LongCount();          
        }

        /// <summary>
        /// Get the total amount of money that has been invoiced
        /// </summary>
        /// <returns>decimal indicating the total amount of money invoiced</returns>
        public decimal GetTotalFundsInvoiced()
        {
            return DbContext.Invoices.Where(invoice => invoice.IsPaid).Select(invoice => invoice.Value).AsEnumerable().Sum();
        }
    }
}
