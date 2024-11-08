using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Konsola
{
    public class StoreService
    {
        private static StoreService _instance;
        public StoreRepository Stock;
        private StoreService()
        {
            Stock = new StoreRepository();
        }

        public static StoreService GetInstance() {
            if (_instance == null)
            {
                _instance = new StoreService();
            }
            return _instance;
        }

        public void AddTax(string Name, int Percentage)
        {
            var taxToAdd = new Tax(Name, Percentage);
            Stock.AddTax(taxToAdd);
        }
        public void DisplayStock() { Stock.DisplayStock(); }
        public void AddToStock(Product product) {  Stock.AddToStock(product); }
    }

}
