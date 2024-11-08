using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sklep_Konsola.OrderRelated;

namespace Sklep_Konsola.AccountRelated
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Basket Basket { get; set; }
        public List<Order> Orders { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public bool Sex { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public Color FavouriteColor { get; set; }
        public Address Address { get; set; }
        public Currency PreferredCurrency { get; set; }
        public List<SpecialDiscount> Discounts { get; set; }
        public bool CorporateClient { get; set; }
        public bool Newsletter { get; set; }

        public Account()
        {
            Orders = new List<Order>();
            Discounts = new List<SpecialDiscount>();
        }
    }

}
