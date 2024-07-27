using E_CommercePraticeModel.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace E_CommercePraticeModel.Models
{
    public class Order
    {
        public int? OrderNo { get; set; }
        public DateTime OrderDate { get; set; }

        
        [InvoicePriceValidator]
        public double InvoicePrice {  get; set; }

        [ProductsListValidator] 
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
