using System;
using System.Collections.Generic;

namespace Sklep_Konsola
{
    public class Product
    {

        public static int _IdCounter = 1;
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Brand? Brand { get; set; }
        public string? Description { get; set; }
        public Price Price { get; set; }
        public string? PhotoPath { get; set; }
        public int Views { get; set; }
        public bool Visible { get; set; }
        public bool New { get; set; }
        public bool InStock { get; set; }
        public int Quantity { get; set; }
        public int TimesBought { get; set; }
        public List<Opinion> Opinions { get; set; } = new List<Opinion>();

        public Product(string Name, Brand brand, int Price, Tax tax, int Quantity, string? Description)
        {
            this.ProductId = _IdCounter++;

            this.Name = Name;
            this.Brand = brand;
            this.Price = new Price(Price, tax);
            this.Quantity = Quantity;

            if (Description != null)
            {
                this.Description = Description;
            }
            PhotoPath = string.Empty;
            this.InStock = true;
            this.TimesBought = 0;
        }
    }
}
