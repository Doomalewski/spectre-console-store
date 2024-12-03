using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Konsola
{
    public class Tax
    {
        public int TaxId { get; set; }
        public string Name { get; set; } 
        public int Value { get; set; }
        public Tax(string Name, int percentage)
        {
            this.Name = Name;
            this.Value = percentage;
        }
    }

    public class Price
    {
        public float FullPrice { get; set; }
        public float? DiscountPrice { get; set; }
        public Tax Vat { get; set; }
        public float NettoPrice;
        public Price(int fullPrice, Tax vat)
        {
            FullPrice = fullPrice;
            Vat = vat;
            NettoPrice = fullPrice/(1+vat.Value);
            DiscountPrice = FullPrice;
        }
    }
}
