using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sklep_Konsola.AccountRelated;

namespace Sklep_Konsola.OrderRelated
{
    public class Order
    {
        public int OrderId { get; set; }
        public Account ClientDetails { get; set; } // Assuming AccountDetails is a class related to Account
        public Address Address { get; set; }
        public List<OrderProduct> Products { get; set; }
        public int ProductsPrice { get; set; }
        public StatusEnum OrderStatus { get; set; }
        public ShippingMethodEnum Shipping { get; set; }
        public PaymentMethod Payment { get; set; }
        public DateTime Date { get; set; }
        public int FullPrice { get; set; }

        public Order()
        {
            Products = new List<OrderProduct>();
        }
    }

}
