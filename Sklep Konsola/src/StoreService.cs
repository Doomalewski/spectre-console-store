using Sklep_Konsola.AccountRelated;
using Sklep_Konsola.OrderRelated;
using Spectre.Console;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Sklep_Konsola
{
    public interface IStoreService
    {
        void AddTax(string name, int percentage);
        void DisplayStock();
        public void AddToStock(Product product);
        public void AdminAddProduct();
        public void SeedBrands();
        public void RegisterUser();
        public List<Product> GetAllProducts();
        public Currency GetCurrencyByName(string name);
        public Account Authenticate(string username, string password);
        public Account GetAccountByUsername(string username);

    }

    public class StoreService : IStoreService
    {
        private static StoreService _instance;
        public StoreRepository Stock;


        private StoreService()
        {
            Stock = StoreRepository.GetInstance();
            SeedBrands();
            SeedCurrencies();
            SeedProducts();
        }

        public static IStoreService GetInstance()
        {
            // Initialize the instance if it's null
            if (_instance == null)
            {
                _instance = new StoreService();
            }
            return _instance;
        }
        public void AddToStock(Product product)
        {
            if (product != null)
            {
                Stock.AddToStock(product);
            }
        }
        public void AddTax(string name, int percentage)
        {
            var taxToAdd = new Tax(name, percentage);
            Stock.AddTax(taxToAdd);
        }

        public void DisplayStock()
        {
            Stock.GetAllProducts();
        }

        public List<Product> GetAllProducts()
        {
            return Stock.GetAllProducts();
        }
        public void AdminAddBrand()
        {
            string brandName = AnsiConsole.Ask<string>("What is the name of the brand?");

            int yearOfFoundation = AnsiConsole.Ask<int>("What is the year of foundation of the brand?");

            string brandDesc = AnsiConsole.Ask<string>("Provide a description of the brand:");

            Brand brand = new Brand(brandName, yearOfFoundation, brandDesc);

            Stock.AddBrand(brand);

            AnsiConsole.MarkupLine("[green]Brand has been successfully added![/]");
        }
        public void AdminAddProduct()
        {

            string productName = AnsiConsole.Ask<string>("What is the name of the product?");

            var brandOptions = Stock.GetAllBrands();
            var brandChoices = brandOptions.Select(b => $"{b.BrandId}: {b.Name}").ToList();

            var selectedBrandChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select the brand for the product:")
                    .AddChoices(brandChoices)
            );

            int selectedBrandId = int.Parse(selectedBrandChoice.Split(':')[0]);

            Brand brand = brandOptions.FirstOrDefault(b => b.BrandId == selectedBrandId);

            if (brand == null)
            {
                AnsiConsole.MarkupLine("[red]Brand not found![/]");
                return; 
            }


            int fullPrice = AnsiConsole.Ask<int>("What is the full price of the product?");
            string taxName = AnsiConsole.Ask<string>("What is the name of the tax (e.g., VAT)?");
            int taxValue = AnsiConsole.Ask<int>("What is the tax percentage?");
            Tax tax = new Tax(taxName, taxValue);

            Price price = new Price(fullPrice, tax);

            int quantity = AnsiConsole.Ask<int>("How many items are available in stock?");

            string? description = AnsiConsole.Ask<string?>("Please provide a description of the product (optional)");

            Product product = new Product(productName, brand, fullPrice, tax, quantity, description);

            Stock.AddToStock(product);

            AnsiConsole.MarkupLine("[green]Product has been added to the stock successfully![/]");
        }
        public void SeedBrands()
        {
            var brand1 = new Brand("Apple", 1976, "Technology company known for iPhones, iPads, and more.");
            var brand2 = new Brand("Samsung", 1938, "South Korean multinational conglomerate, known for electronics and appliances.");
            var brand3 = new Brand("Nike", 1964, "American multinational corporation that designs footwear, apparel, and sports equipment.");
            var brand4 = new Brand("Dell", 1984, "American multinational computer technology company, known for its laptops and desktops.");
            var brand5 = new Brand("OnePlus", 2013, "Chinese smartphone manufacturer, known for flagship phones.");
            var brand6 = new Brand("Fitbit", 2007, "Fitness tracking company, known for wearable devices.");
            var brand7 = new Brand("GoPro", 2002, "Company specializing in action cameras and accessories.");
            var brand8 = new Brand("Nintendo", 1889, "Japanese multinational company, known for gaming consoles and games.");
            var brand9 = new Brand("Kindle", 2007, "E-book reader brand developed by Amazon.");
            var brand10 = new Brand("Bose", 1964, "American company, known for high-quality audio products.");

            Stock.AddBrand(brand1);
            Stock.AddBrand(brand2);
            Stock.AddBrand(brand3);
            Stock.AddBrand(brand4);
            Stock.AddBrand(brand5);
            Stock.AddBrand(brand6);
            Stock.AddBrand(brand7);
            Stock.AddBrand(brand8);
            Stock.AddBrand(brand9);
            Stock.AddBrand(brand10);

            AnsiConsole.MarkupLine("[green]Initial brands have been seeded successfully![/]");
        }

        public void SeedCurrencies()
        {
            var currencies = new List<Currency>
        {
            new Currency { Name = "USD", Symbol = "$", PlnToCurrRatio = 3.80f },
            new Currency { Name = "EUR", Symbol = "€", PlnToCurrRatio = 4.50f },
            new Currency { Name = "GBP", Symbol = "£", PlnToCurrRatio = 5.20f },
            new Currency { Name = "YEN", Symbol = "¥", PlnToCurrRatio = 4.30f },
            new Currency { Name = "PLN", Symbol = "pln", PlnToCurrRatio = 1.0f } // Dla PLN, stosunek PLN do PLN wynosi 1
        };

            foreach (var currency in currencies)
            {
                Stock.AddCurrency(currency);
            }

            AnsiConsole.MarkupLine("[green]Currencies seeded succefully![/]");
        }
        public void SeedProducts()
        {
            var brands = Stock.GetAllBrands();

            var product1 = new Product("iPhone 13", brands[0], 3999, new Tax("VAT", 23), 50, "Latest iPhone model with 128GB storage.");
            var product2 = new Product("Galaxy S21", brands[1], 3499, new Tax("VAT", 23), 30, "Flagship Samsung phone with 128GB storage.");
            var product3 = new Product("AirMax 90", brands[2], 699, new Tax("VAT", 23), 100, "Comfortable running shoes with iconic design.");
            var product4 = new Product("MacBook Pro M1", brands[0], 8999, new Tax("VAT", 23), 20, "Powerful laptop with M1 chip and 512GB SSD.");
            var product5 = new Product("Samsung Galaxy Tab S7", brands[1], 2499, new Tax("VAT", 23), 60, "High-performance tablet with 11-inch display.");
            var product6 = new Product("Sony WH-1000XM4", brands[2], 1299, new Tax("VAT", 23), 75, "Noise-canceling wireless headphones.");
            var product7 = new Product("Dell XPS 13", brands[3], 6999, new Tax("VAT", 23), 15, "Compact and powerful laptop with 13.3-inch display.");
            var product8 = new Product("OnePlus 9", brands[4], 3199, new Tax("VAT", 23), 50, "Flagship phone with 5G and 128GB storage.");
            var product9 = new Product("Fitbit Charge 5", brands[5], 899, new Tax("VAT", 23), 200, "Advanced fitness tracker with heart rate monitor.");
            var product10 = new Product("GoPro HERO10", brands[6], 2499, new Tax("VAT", 23), 40, "Waterproof action camera with 5.3K video resolution.");
            var product11 = new Product("Nintendo Switch", brands[7], 1499, new Tax("VAT", 23), 80, "Hybrid gaming console with 32GB storage.");
            var product12 = new Product("Kindle Paperwhite", brands[8], 599, new Tax("VAT", 23), 150, "E-reader with 6-inch display and built-in light.");
            var product13 = new Product("Apple Watch Series 7", brands[0], 2499, new Tax("VAT", 23), 40, "Smartwatch with ECG and fitness tracking.");
            var product14 = new Product("Bose SoundLink Revolve", brands[9], 999, new Tax("VAT", 23), 100, "Portable Bluetooth speaker with 360-degree sound.");
            var product15 = new Product("iPad Air 4", brands[0], 2999, new Tax("VAT", 23), 50, "Stylish tablet with 10.9-inch display and A14 Bionic chip.");

            Stock.AddToStock(product1);
            Stock.AddToStock(product2);
            Stock.AddToStock(product3);
            Stock.AddToStock(product4);
            Stock.AddToStock(product5);
            Stock.AddToStock(product6);
            Stock.AddToStock(product7);
            Stock.AddToStock(product8);
            Stock.AddToStock(product9);
            Stock.AddToStock(product10);
            Stock.AddToStock(product11);
            Stock.AddToStock(product12);
            Stock.AddToStock(product13);
            Stock.AddToStock(product14);
            Stock.AddToStock(product15);

            AnsiConsole.MarkupLine("[green]All products have been successfully added to the stock![/]");

        }

        public void AddAccount(Account account)
        {
            if (Stock.GetAllAccounts().Any(a => a.Email.Equals(account.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Email is already in use.");
            }

            Stock.AddAccount(account);
        }
        public void AddUser(string username, string email, string password, string name, string surname, int age, bool sex, int height, int weight, Sklep_Konsola.AccountRelated.Color favouriteColor, Address address, Currency preferredCurrency)
        {
            if (!Stock.IsEmailUnique(email))
            {
                AnsiConsole.MarkupLine("[red]Error: This email is already in use![/]");
                return;
            }

            Account newAccount = new Account
            {
                Username = username,
                Email = email,
                Password = password,  
                Name = name,
                Surname = surname,
                Age = age,
                Sex = sex,
                Height = height,
                Weight = weight,
                FavouriteColor = favouriteColor,
                Address = address,
                PreferredCurrency = preferredCurrency,
                Basket = new Basket(),
                Orders = new List<Order>()
            };

            Stock.AddAccount(newAccount);

            AnsiConsole.MarkupLine("[green]User registered successfully![/]");
        }
        public void RegisterUser()
        {
            string email;
            do
            {
                email = AnsiConsole.Ask<string>("[bold]Enter your email (must be unique):[/]");
                if (!Stock.IsEmailUnique(email))
                {
                    AnsiConsole.MarkupLine("[red]Error: This email is already in use![/]");
                }
            } while (!Stock.IsEmailUnique(email));

            string username = AnsiConsole.Ask<string>("[bold]Enter your username:[/]");

            string password = AnsiConsole.Ask<string>("[bold]Enter your password:[/]");

            bool completeProfileNow = AnsiConsole.Confirm("[bold]Do you want to complete your profile now?[/]");

            Account newAccount = new Account
            {
                Username = username,
                Email = email,
                Password = password,
            };

            if (completeProfileNow)
            {
                newAccount.Name = AnsiConsole.Ask<string>("[bold]Enter your first name:[/]");
                newAccount.Surname = AnsiConsole.Ask<string>("[bold]Enter your last name:[/]");
                newAccount.Age = AnsiConsole.Ask<int>("[bold]Enter your age:[/]");
                newAccount.Sex = AnsiConsole.Confirm("[bold]Are you male?[/]");
                newAccount.Height = AnsiConsole.Ask<int>("[bold]Enter your height (cm):[/]");
                newAccount.Weight = AnsiConsole.Ask<int>("[bold]Enter your weight (kg):[/]");

                newAccount.FavouriteColor = AnsiConsole.Prompt(
                    new SelectionPrompt<Sklep_Konsola.AccountRelated.Color>()
                        .Title("[bold]Select your favourite color:[/]")
                        .AddChoices(Enum.GetValues(typeof(Sklep_Konsola.AccountRelated.Color)).Cast<Sklep_Konsola.AccountRelated.Color>())
                );

                newAccount.Address = new Address(
                    AnsiConsole.Ask<string>("[bold]Enter your street address:[/]"),
                    AnsiConsole.Ask<string>("[bold]Enter your city:[/]"),
                    AnsiConsole.Ask<string>("[bold]Enter your state:[/]"),
                    AnsiConsole.Ask<string>("[bold]Enter your postal code:[/]"),
                    AnsiConsole.Ask<string>("[bold]Enter your country:[/]")
                );

                newAccount.PreferredCurrency = AnsiConsole.Prompt(
                    new SelectionPrompt<Currency>()
                        .Title("[bold]Select your preferred currency:[/]")
                        .AddChoices(Stock.GetAllCurrencies())
                        .UseConverter(currency => $"{currency.Symbol} - {currency.Name}")
                );
            }
            else
            {
                newAccount.Name = "";
                newAccount.Surname = "";
                newAccount.Age = 0;
                newAccount.Sex = false;
                newAccount.Height = 0;
                newAccount.Weight = 0;
                newAccount.FavouriteColor = Sklep_Konsola.AccountRelated.Color.Red;
                newAccount.Address = new Address("", "","", "", "");
                newAccount.PreferredCurrency = Stock.GetCurrencyByName("PLN");
            }

            Stock.AddAccount(newAccount);
            Console.Clear();
            AnsiConsole.MarkupLine("[green]User registered successfully![/]");
        }
        public Currency GetCurrencyByName(string name)
        {
            return Stock.GetCurrencyByName(name);
        }
        public Account Authenticate(string username, string password)
        {
            var account = Stock.GetAllAccounts().FirstOrDefault(a => a.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (account == null)
            {
                return null;
            }

            if (account.Password == password)
            {
                return account;
            }

            return null;
        }
        public Account GetAccountByUsername(string username)
        {
            return Stock.Accounts.FirstOrDefault(p => p.Username.Equals(username));
        }
    }
}
