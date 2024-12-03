using Sklep_Konsola.AccountRelated;
using Sklep_Konsola.OrderRelated;
using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sklep_Konsola
{
    public class ViewController
    {
        private static ViewController _instance;
        private Account _currentAccount;
        private IStoreService _storeService;
        public Currency Currency { get; private set; }
        public Basket Cart { get; private set; }
        private ViewController(IStoreService storeService) {
            _storeService = storeService;
            Currency = storeService.GetCurrencyByName("PLN");
            Cart = new Basket();
        }

        public static ViewController GetInstance(IStoreService storeService)
        {
            if (_instance == null)
            {
                _instance = new ViewController(storeService);
            }
            return _instance;
        }
        public void SetCurrentAccount(Account account)
        {
            _currentAccount = account;
        }
        public bool IsLoggedIn()
        {
            return _currentAccount != null;
        }
        public void DisplayMainMenu()
        {
            AnsiConsole.MarkupLine(@"
                                                       ,----,                                       
                                                    , /   .`|     ,----..                           
                ,---,.              .--.--.        ,`   .'  :    /   /   \   ,-.----.        ,---,. 
              ,'  .' |             /  /    '.    ;    ;     /   /   .     :  \    /  \     ,'  .' | 
            ,---.'   |     ,---,. |  :  /`. /  .'___,/    ,'   .   /   ;.  \ ;   :    \  ,---.'   | 
            |   |   .'   ,'  .' | ;  |  |--`   |    :     |   .   ;   /  ` ; |   | .\ :  |   |   .' 
            :   :  |-, ,---.'   , |  :  ;_     ;    |.';  ;   ;   |  ; \ ; | .   : |: |  :   :  |-, 
            :   |  ;/| |   |    |  \  \    `.  `----'  |  |   |   :  | ; | ' |   |  \ :  :   |  ;/| 
            |   :   .' :   :  .'    `----.   \     '   :  ;   .   |  ' ' ' : |   : .  /  |   :   .' 
            |   |  |-, :   |.'      __ \  \  |     |   |  '   '   ;  \; /  | ;   | |  \  |   |  |-, 
            '   :  ;/| `---'       /  /`--'  /     '   :  |    \   \  ',  /  |   | ;\  \ '   :  ;/| 
            |   |    \            '--'.     /      ;   |.'      ;   :    /   :   ' | \.' |   |    \ 
            |   :   .'              `--'---'       '---'         \   \ .'    :   : :-'   |   :   .' 
            |   | ,'                                              `---`      |   |.'     |   | ,'   
            `----'                                                           `---'       `----'     
            ");
            if (IsLoggedIn())
            {
                var selectedOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"[bold]Welcome, {_currentAccount.Username}![/]")
                        .AddChoices("View Products", "View Orders", "View Cart", "Log Out")
                );

                switch (selectedOption)
                {
                    case "View Products":
                        ViewProducts();
                        break;
                    case "View Orders":
                        ViewOrders();
                        break;
                    case "View Cart":
                        ViewCart();
                        break;
                    case "Log Out":
                        LogOut();
                        break;
                }
            }
            else
            {
                var selectedOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Welcome to the Shop![/]")
                        .AddChoices("View Products", "View Cart", "Log In", "Register")
                );

                switch (selectedOption)
                {
                    case "View Products":
                        ViewProducts();
                        break;
                    case "View Cart":
                        ViewCart();
                        break;
                    case "Log In":
                        LogIn();
                        break;
                    case "Register":
                        _storeService.RegisterUser();
                        break;
                }
            }
        }
        private void LogOut()
        {
            _currentAccount = null;
            Console.Clear();
            DisplayMainMenu();
        }
        private void ViewProducts()
        {
            var products = _storeService.GetAllProducts();

            if (products.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No products available.[/]");
                return;
            }

            var table = new Table();
            table.AddColumn("Product ID");
            table.AddColumn("Name");
            table.AddColumn("Brand");
            table.AddColumn("Price");
            table.AddColumn("Quantity");
            table.AddColumn("In Stock");

            if (IsLoggedIn())
            {
                Currency = _currentAccount.PreferredCurrency;
            }

            foreach (var product in products)
            {
                table.AddRow(
                    product.ProductId.ToString(),
                    product.Name,
                    product.Brand?.Name ?? "No Brand",
                    $"{product.Price.FullPrice * Currency.PlnToCurrRatio} {Currency.Symbol}",
                    product.Quantity.ToString(),
                    product.InStock ? "[green]Yes[/]" : "[red]No[/]"
                );
            }

            AnsiConsole.Write(table);

            while (true)
            {
                var action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Choose an option:[/]")
                        .AddChoices(
                            "Search for a product",
                            "View Product Details",
                            "Add Product to Cart",
                            "Sort Products",
                            "Return to Main Menu"
                        )
                );

                switch (action)
                {
                    case "Search for a product":
                        var searchQuery = AnsiConsole.Prompt(
                            new TextPrompt<string>("[bold]Enter a product name to search for (optional):[/]")
                                .AllowEmpty()
                        );

                        if (!string.IsNullOrWhiteSpace(searchQuery))
                        {
                            products = products.Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
                        }

                        if (products.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[red]No products found matching the search query.[/]");
                            return;
                        }

                        table = new Table();
                        table.AddColumn("Product ID");
                        table.AddColumn("Name");
                        table.AddColumn("Brand");
                        table.AddColumn("Price");
                        table.AddColumn("Quantity");
                        table.AddColumn("In Stock");

                        foreach (var product in products)
                        {
                            table.AddRow(
                                product.ProductId.ToString(),
                                product.Name,
                                product.Brand?.Name ?? "No Brand",
                                $"{product.Price.FullPrice * Currency.PlnToCurrRatio} {Currency.Symbol}",
                                product.Quantity.ToString(),
                                product.InStock ? "[green]Yes[/]" : "[red]No[/]"
                            );
                        }

                        Console.Clear();
                        AnsiConsole.Write(table);

                        break;

                    case "View Product Details":
                        var selectedProductId = AnsiConsole.Prompt(
                            new SelectionPrompt<int>()
                                .Title("[bold]Select a product to view details:[/]")
                                .PageSize(10)
                                .AddChoices(products.Select(p => p.ProductId).ToArray())
                        );

                        var selectedProduct = products.FirstOrDefault(p => p.ProductId == selectedProductId);

                        if (selectedProduct != null)
                        {
                            ShowProductDetails(selectedProduct);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Product not found.[/]");
                        }
                        break;

                    case "Add Product to Cart":
                        var productIdToAdd = AnsiConsole.Prompt(
                            new SelectionPrompt<int>()
                                .Title("[bold]Select a product to add to the cart:[/]")
                                .PageSize(10)
                                .AddChoices(products.Select(p => p.ProductId).ToArray())
                        );

                        var productToAdd = products.FirstOrDefault(p => p.ProductId == productIdToAdd);

                        if (productToAdd != null)
                        {
                            var quantityToAdd = AnsiConsole.Prompt(
                                new TextPrompt<int>("[bold]Enter the quantity to add to the cart:[/]")
                                    .ValidationErrorMessage("[red]Invalid quantity.[/]")
                                    .Validate(qty => qty > 0 && qty <= productToAdd.Quantity)
                            );

                            Cart.AddProduct(productToAdd, quantityToAdd);
                            AnsiConsole.MarkupLine($"[green]Added {quantityToAdd} of {productToAdd.Name} to your cart![/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Product not found.[/]");
                        }
                        break;

                    case "Sort Products":
                        var sortAction = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[bold]How would you like to sort the products?[/]")
                                .AddChoices("Sort by Price", "Sort by Availability")
                        );

                        switch (sortAction)
                        {
                            case "Sort by Price":
                                products = products.OrderBy(p => p.Price.FullPrice).ToList();
                                AnsiConsole.MarkupLine("[green]Products sorted by price.[/]");
                                break;

                            case "Sort by Availability":
                                products = products.OrderBy(p => p.Quantity).ToList();
                                AnsiConsole.MarkupLine("[green]Products sorted by availability.[/]");
                                break;
                        }

                        table = new Table();
                        table.AddColumn("Product ID");
                        table.AddColumn("Name");
                        table.AddColumn("Brand");
                        table.AddColumn("Price");
                        table.AddColumn("Quantity");
                        table.AddColumn("In Stock");

                        foreach (var product in products)
                        {
                            table.AddRow(
                                product.ProductId.ToString(),
                                product.Name,
                                product.Brand?.Name ?? "No Brand",
                                $"{product.Price.FullPrice * Currency.PlnToCurrRatio} {Currency.Symbol}",
                                product.Quantity.ToString(),
                                product.InStock ? "[green]Yes[/]" : "[red]No[/]"
                            );
                        }

                        Console.Clear();
                        AnsiConsole.Write(table);
                        break;

                    case "Return to Main Menu":
                        Console.Clear();
                        DisplayMainMenu();
                        return;
                }
            }
        }
        private void ViewCart()
        {
            var cartItems = Cart.Products;
            Console.Clear();

            if (!cartItems.Any())
            {
                AnsiConsole.MarkupLine("[red]Your cart is empty.[/]");
                return;
            }

            var table = new Table();
            table.AddColumn("Product ID");
            table.AddColumn("Name");
            table.AddColumn("Brand");
            table.AddColumn("Quantity");
            table.AddColumn("Unit Price");
            table.AddColumn("Total Price");

            decimal totalCost = 0;

            foreach (var item in cartItems)
            {
                var product = item.Product;
                var quantity = item.Quantity;
                var unitPrice = product.Price.FullPrice * Currency.PlnToCurrRatio;
                var totalPrice = unitPrice * quantity;

                totalCost += (decimal)totalPrice;

                table.AddRow(
                    product.ProductId.ToString(),
                    product.Name,
                    product.Brand?.Name ?? "No Brand",
                    quantity.ToString(),
                    $"{unitPrice:F2} {Currency.Symbol}",
                    $"{totalPrice:F2} {Currency.Symbol}"
                );
            }

            AnsiConsole.Write(table);

            AnsiConsole.Write(
                new Markup($"[bold green]Total Cost: [/][bold]{(decimal)totalCost:F2} {Currency.Symbol}[/]")
            );



            while (true)
            {
                var action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Choose an option:[/]")
                        .AddChoices("Checkout", "Remove Item", "Return to Main Menu")
                );

                switch (action)
                {
                    case "Checkout":
                        Checkout();
                        return;

                    case "Remove Item":
                        var productIdToRemove = AnsiConsole.Prompt(
                            new SelectionPrompt<int>()
                                .Title("[bold]Select a product to remove from the cart:[/]")
                                .PageSize(10)
                                .AddChoices(cartItems.Select(p => p.Product.ProductId).ToArray())
                        );

                        var itemToRemove = cartItems.FirstOrDefault(p => p.Product.ProductId == productIdToRemove);

                        if (itemToRemove != null)
                        {
                            cartItems.Remove(itemToRemove);
                            AnsiConsole.MarkupLine($"[red]Removed {itemToRemove.Product.Name} from your cart.[/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Product not found in the cart.[/]");
                        }
                        break;

                    case "Return to Main Menu":
                        Console.Clear();
                        DisplayMainMenu();
                        return;
                }
            }
        }
        private void Checkout()
        {
            if (!IsLoggedIn())
            {
                AnsiConsole.MarkupLine("[red]You need to log in to proceed with checkout.[/]");
                LogIn();
                return;
            }

            if (Cart.Products.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Your cart is empty. Add some products to proceed with checkout.[/]");
                return;
            }

            var totalCost = 0m;
            AnsiConsole.MarkupLine("[bold]Order Summary:[/]");
            foreach (var orderProduct in Cart.Products)
            {
                var product = orderProduct.Product;
                var quantity = orderProduct.Quantity;
                var unitPrice = product.Price.FullPrice * Currency.PlnToCurrRatio;
                var totalPrice = unitPrice * quantity;

                totalCost += (decimal)totalPrice;

                AnsiConsole.MarkupLine($"- {quantity}x {product.Name} at {unitPrice:F2} {Currency.Symbol} each. Total: {totalPrice:F2} {Currency.Symbol}");
            }
            AnsiConsole.MarkupLine($"[bold green]Total Cost: {totalCost:F2} {Currency.Symbol}[/]");

            var confirm = AnsiConsole.Confirm("Do you want to place the order?");
            if (!confirm)
            {
                AnsiConsole.MarkupLine("[yellow]Order canceled.[/]");
                return;
            }

            foreach (var orderProduct in Cart.Products)
            {
                var product = orderProduct.Product;
                product.Quantity -= orderProduct.Quantity;

                if (product.Quantity <= 0)
                {
                    product.InStock = false;
                }
            }

            _currentAccount.Orders.Add(new Order(_currentAccount, Cart.Products));
            _currentAccount.Age = 10;
            Cart.Products.Clear();
            Console.Clear();
            AnsiConsole.MarkupLine("[green bold]Order placed successfully![/]");
            AnsiConsole.MarkupLine("[bold]Thank you for shopping with us![/]");
        }
        private void LogIn()
        {
            AnsiConsole.MarkupLine("[bold]Log In to Your Account[/]");

            var username = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Enter your username:[/]")
                    .ValidationErrorMessage("[red]Username cannot be empty.[/]")
                    .Validate(input => !string.IsNullOrWhiteSpace(input))
            );

            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Enter your password:[/]")
                    .Secret()
                    .ValidationErrorMessage("[red]Password cannot be empty.[/]")
                    .Validate(input => !string.IsNullOrWhiteSpace(input))
            );

            var account = _storeService.Authenticate(username, password);
            if (account != null)
            {
                _currentAccount = account;
                Console.Clear();
                AnsiConsole.MarkupLine($"[green]Welcome back, {username}![/]");
                DisplayMainMenu();

            }
            else
            {
                var action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[red]Invalid username or password. Please try again or return to the main menu.[/]")
                        .AddChoices("Try Again", "Return to Main Menu")
                );

                if (action == "Try Again")
                {
                    LogIn();
                }
                else if (action == "Return to Main Menu")
                {
                    Console.Clear();
                    DisplayMainMenu();
                }
            }

        }
        private void ViewOrders()
        {
            Console.Clear();
            if (!IsLoggedIn())
            {
                AnsiConsole.MarkupLine("[red]You need to log in to view your orders.[/]");
                LogIn(); 
                return;
            }

            var orders = _currentAccount.Orders;

            if (orders.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]You have no orders yet.[/]");
                return;
            }

            var table = new Table();
            table.AddColumn("Order ID");
            table.AddColumn("Order Date");
            table.AddColumn("Total Price");
            table.AddColumn("Status");

            foreach (var order in orders)
            {
                var totalPrice = order.ProductsPrice * Currency.PlnToCurrRatio;
                var status = "Completed"; 

                table.AddRow(
                    order.OrderId.ToString(),
                    DateTime.Now.ToString(),
                    $"{totalPrice:F2} {Currency.Symbol}",
                    status
                );
            }

            AnsiConsole.Render(table);

            var action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Choose an option:[/]")
                    .AddChoices("Return to Main Menu")
            );

            switch (action)
            {
                case "Return to Main Menu":
                    Console.Clear();
                    DisplayMainMenu();
                    break;
            }
        }
        private void ShowProductDetails(Product product)
        {
            AnsiConsole.Clear();

            AnsiConsole.MarkupLine($"[bold]Product Details for {product.Name}[/]");
            AnsiConsole.MarkupLine($"[bold]Brand:[/] {product.Brand.Name}");
            AnsiConsole.MarkupLine($"[bold]Description:[/] {product.Description ?? "No description available."}");
            AnsiConsole.MarkupLine($"[bold]Price:[/] {product.Price.FullPrice} {Currency.Symbol}");
            AnsiConsole.MarkupLine($"[bold]Quantity:[/] {product.Quantity}");

            AnsiConsole.MarkupLine($"[bold]In Stock:[/] {(product.InStock ? "[green]Yes[/]" : "[red]No[/]")}");


            AnsiConsole.MarkupLine($"[bold]Times Bought:[/] {product.TimesBought}");
            AnsiConsole.MarkupLine($"[bold]Views:[/] {product.Views}");


            ViewProducts();
        }
    }
}
