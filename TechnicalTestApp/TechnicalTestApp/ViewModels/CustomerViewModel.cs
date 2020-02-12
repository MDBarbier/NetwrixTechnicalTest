namespace TechnicalTestApp.ViewModels
{
    public class CustomerViewModel
    {
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public int MostRecentInvoiceRef { get; set; }
        public decimal MostRecentInvoiceAmount { get; set; }
        public decimal TotalOfAllOutstandingInvoices { get; set; }
        public decimal TotalOfAllPaidInvoices { get; set; }
        public long NumberOfOutstandingInvoices { get; set; }
    }
}
