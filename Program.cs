using ColombianCoffee.Src.Shared.Helpers;
using ColombianCoffee.Src.Shared.Contexts;
using ColombianCoffee.Src.Modules.MainMenu;

namespace ColombianCoffee;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var dbContext = DbContextFactory.Create();

            if (dbContext != null)
            {
                var mainMenu = new MainMenu(dbContext);
                mainMenu.Show();
            }
            else
            {
                Console.WriteLine("Error al crear el contexto de la base de datos");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}