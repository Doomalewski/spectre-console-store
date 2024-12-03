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
        private static int lastOrderId = 0;

        public int OrderId { get; private set; }
        public Account ClientDetails { get; set; }
        public List<OrderProduct> Products { get; set; }
        public int ProductsPrice { get; set; }

        public Order(Account clientDetails, List<OrderProduct> products)
        {
            lastOrderId++;
            OrderId = lastOrderId;

            ClientDetails = clientDetails;
            Products = products ?? new List<OrderProduct>();
            ProductsPrice = (int)CalculateTotalPrice();
        }

        private float CalculateTotalPrice()
        {
            return Products.Sum(product => product.Product.Price.FullPrice * product.Quantity);
        }
    }

}
