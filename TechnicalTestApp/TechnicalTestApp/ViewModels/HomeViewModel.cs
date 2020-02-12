using System.Collections.Generic;

namespace TechnicalTestApp.ViewModels
{
    public class HomeViewModel
    {
        public decimal PaidInvoiceTotal { get; set; }
        public long TotalPaidInvoiceCount { get; set; }
        public Dictionary<int, CustomerViewModel> Customers { get; set; }
    }
}
