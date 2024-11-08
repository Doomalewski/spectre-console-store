using Sklep_Konsola.AccountRelated;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Konsola
{
    public class StoreRepository
    {
        public List<Product> ProductStock { get; private set; }
        public List<Tax> Taxes { get; private set; }
        public List<Opinion> Opinions { get; private set; }
        public List<Currency> Currencies;


        public StoreRepository()
        {
            ProductStock = new List<Product>();
            Taxes = new List<Tax>();
            Opinions = new List<Opinion>();
            Currencies = new List<Currency>();
        }

        public void AddToStock(Product product)
        {
            if (product != null)
                ProductStock.Add(product);
        }
        public void AddTax(Tax tax) { 
            if (tax != null) 
                Taxes.Add(tax); }
        public void AddOpinion(Opinion opinion) {
            if (opinion != null) 
                Opinions.Add(opinion); }

        public void DisplayStock()
        {
            if (ProductStock.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No products in stock.[/]");
            }
            else
            {
                var table = new Table();
                table.AddColumn("[bold yellow]Product Name[/]");
                table.AddColumn("[bold yellow]Quantity[/]");
                table.AddColumn("[bold yellow]Price[/]");

                foreach (var product in ProductStock)
                {
                    table.AddRow(product.Name, product.Quantity.ToString(), product.Price.ToString());
                }

                AnsiConsole.Write(table);
            }
        }
    }
}
