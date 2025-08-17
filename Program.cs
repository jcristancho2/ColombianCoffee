using ColombianCoffee.Src.Shared.Helpers;
using ColombianCoffee.Src.Shared.Contexts;
using ColombianCoffee.Src.Modules.MainMenu;

namespace ColombianCoffee;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var dbContext = DbContextFactory.Create();
            var mainMenu = new MainMenu(dbContext);
            await mainMenu.Show();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}