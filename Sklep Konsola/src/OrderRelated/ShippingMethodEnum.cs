using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Konsola.OrderRelated
{
    public class ShippingMethodEnum
    {
        public int ShippingMethodId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string EstimatedDeliveryTime { get; set; }
    }

}
