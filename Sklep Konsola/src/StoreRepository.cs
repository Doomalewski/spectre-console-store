using Sklep_Konsola.AccountRelated;
using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Konsola
{
    public class StoreRepository
    {
        private static StoreRepository _instance;

        public List<Product> Products { get; private set; }
        public List<Tax> Taxes { get; private set; }
        public List<Opinion> Opinions { get; private set; }
        public List<Currency> Currencies { get; private set; }
        public List<Brand> Brands { get; private set; }
        public List<Account> Accounts { get; private set; } = new List<Account>();

        private StoreRepository()
        {
            Products = new List<Product>();
            Taxes = new List<Tax>();
            Opinions = new List<Opinion>();
            Currencies = new List<Currency>();
            Brands = new List<Brand>(); 
        }

        public static StoreRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new StoreRepository();
            }

            return _instance;
        }

        public void AddToStock(Product product)
        {
            if (product != null)
                Products.Add(product);
        }
        public void AddAccount(Account account)
        {
            if (account != null)
            {
                Accounts.Add(account);
            }
        }

        public List<Account> GetAllAccounts()
        {
            return Accounts.ToList();
        }
        public void AddTax(Tax tax)
        {
            if (tax != null)
                Taxes.Add(tax);
        }

        public void AddOpinion(Opinion opinion)
        {
            if (opinion != null)
                Opinions.Add(opinion);
        }

        public void AddBrand(Brand brand)
        {
            if (brand != null)
            {
                Brands.Add(brand);
            }
        }

        public List<Brand> GetAllBrands()
        {
            return Brands;
        }

        public bool IsEmailUnique(string email)
        {
            return !Accounts.Any(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
        public void AddCurrency(Currency currency)
        {
            if (currency != null)
            {
                Currencies.Add(currency);
            }
        }
        public List<Currency> GetAllCurrencies()
        {

            return Currencies.ToList();
        }
        public Currency GetCurrencyByName(string name)
        {
            return Currencies.FirstOrDefault(e=>e.Name == name);
        }
        public List<Product> GetAllProducts()
        {
            return Products.ToList();
        }
    }

}