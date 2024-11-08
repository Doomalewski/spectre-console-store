using Sklep_Konsola;
using Spectre.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        var storeManager = StoreService.GetInstance();
        storeManager.InitRepository();

        while (true)
        {
            Console.Clear();
            DisplayMainScreen();
            int choice = AnsiConsole.Prompt(
                new SelectionPrompt<int>()
                    .Title("[green]Choose an option:[/]")
                    .AddChoices(new[] { 1, 2, 3 })
                    .UseConverter(choice => choice switch
                    {
                        1 => "View Products",
                        2 => "Add Product to Stock",
                        3 => "Exit",
                        _ => string.Empty
                    })
            );

            switch (choice)
            {
                case 1:
                    storeManager.Stock.DisplayStock();
                    break;
                case 2:
                    var product = CreateProduct();
                    storeManager.AddProduct(product);
                    storeManager.AddToStock(product);
                    AnsiConsole.MarkupLine("[green]Product added successfully![/]");
                    break;
                case 3:
                    return;
            }

            AnsiConsole.MarkupLine("\n[bold]Press any key to continue...[/]");
            Console.ReadKey();
        }
    }

    private static void DisplayMainScreen()
    {
        var panel = new Panel("[bold green]Welcome to the Aura Store[/]");
        panel.Header = new PanelHeader("[blue]Main Menu[/]").Justify(Justify.Center);
        panel.Border = BoxBorder.Double;
        panel.Padding = new Padding(2, 2, 2, 2);
        panel.Expand = true;

        AnsiConsole.Write(panel);
    }

    private static Product CreateProduct()
    {
        var name = AnsiConsole.Ask<string>("[yellow]Enter product name:[/]");
        var quantity = AnsiConsole.Ask<int>("[yellow]Enter quantity:[/]");
        var price = AnsiConsole.Ask<decimal>("[yellow]Enter price:[/]");

        return new Product { Name = name, Quantity = quantity, Price = price };
    }
}