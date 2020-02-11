using System;

namespace TechnicalTestApp.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public string Ref { get; set; }
        public DateTime InvoiceDate { get; set; }
        public bool IsPaid { get; set; }
        public decimal Value { get; set; }        
    }
}
