using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sklep_Konsola.OrderRelated;

namespace Sklep_Konsola.AccountRelated
{
    public class Basket
    {
        public List<OrderProduct> Products { get; set; }

        public Basket()
        {
            Products = new List<OrderProduct>();
        }

        public void AddProduct(Product product)
        {
            if (product != null)
            {
                Products.Add(new OrderProduct { Product = product });
            }
        }

        public void RemoveProduct(Product product)
        {
            Products.RemoveAll(p => p.Product == product);
        }

        public decimal GetTotalPrice()
        {
            return Products.Sum(p => p.Product.Price); // Assuming Product has a Price property
        }
    }

}
