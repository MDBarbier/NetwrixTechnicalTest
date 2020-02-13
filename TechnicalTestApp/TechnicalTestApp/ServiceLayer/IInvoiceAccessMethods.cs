using TechnicalTestApp.Database;
using TechnicalTestApp.Models;

namespace TechnicalTestApp.ServiceLayer
{
    public interface IInvoiceAccessMethods
    {
        public IApplicationDatabaseContext DbContext { get; }

        public abstract Invoice GetInvoiceById(int invoiceId);

        public abstract long GetSumOfInvoicesHeld(bool paidInvoicesOnly);

        public abstract decimal GetTotalFundsInvoiced();

        public abstract long GetNumberOfOutstandingInvoicesForCustomer(int customerId);

        public abstract decimal GetAmountOwedOnInvoices(int customerId, bool paidOnly);

        public abstract int GetMostRecentInvoiceRef(int customerId);

        public abstract decimal GetMostRecentInvoiceAmount(int customerId);

    }
}
