using Sklep_Konsola;
using Spectre.Console;

public static class Program
{
    public static void Main(string[] args)
    {
        var storeManager = StoreService.GetInstance();
        var viewManager = ViewController.GetInstance(storeManager);
        while (true)
        {
            viewManager.DisplayMainMenu();
        }
    }
}