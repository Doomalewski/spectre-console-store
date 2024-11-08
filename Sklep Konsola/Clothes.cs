using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Konsola
{
    public class Tops : Product
    {
        public TopsCategory TopsType { get; set; }

        // Constructor for Tops class
        public Tops(string name, Brand brand, int price, Tax tax, int quantity, string? description, TopsCategory topsType)
            : base(name, brand, price, tax, quantity, description) // Calls the base Product constructor
        {
            TopsType = topsType;
        }
    }

    public class Bottoms : Product
    {
        public BottomsCategory BottomsType { get; set; }

        // Constructor for Bottoms class
        public Bottoms(string name, Brand brand, int price, Tax tax, int quantity, string? description, BottomsCategory bottomsType)
            : base(name, brand, price, tax, quantity, description) // Calls the base Product constructor
        {
            BottomsType = bottomsType;
        }
    }

    public class Shoes : Product
    {
        public ShoesCategory ShoesType { get; set; }

        // Constructor for Shoes class
        public Shoes(string name, Brand brand, int price, Tax tax, int quantity, string? description, ShoesCategory shoesType)
            : base(name, brand, price, tax, quantity, description) // Calls the base Product constructor
        {
            ShoesType = shoesType;
        }
    }

    public class Accessory : Product
    {
        public AccessoryCategory AccessoryType { get; set; }

        // Constructor for Accessory class
        public Accessory(string name, Brand brand, int price, Tax tax, int quantity, string? description, AccessoryCategory accessoryType)
            : base(name, brand, price, tax, quantity, description) // Calls the base Product constructor
        {
            AccessoryType = accessoryType;
        }
    }

}
