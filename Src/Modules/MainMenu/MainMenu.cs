using ColombianCoffee.Src.Shared.Contexts;
using ColombianCoffee.Src.Shared.Utils;
using Spectre.Console;

namespace ColombianCoffee.Src.Modules.MainMenu;

public class MainMenu
{
    private readonly AppDbContext _dbContext;

    public MainMenu(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(
                new FigletText("Colombian Coffee")
                .Centered()
                .Color(Color.Green)
            );

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Select an option")
                .AddChoices(new[] {
                    "Sign up",
                    "Log in",
                    "Exit"
                })
            );

            switch (selection)
            {
                case "Sign up":
                    Console.Clear();
                    AnsiConsole.WriteLine("Funcionalidad de registro");
                    ScreenController.PauseScreen();
                    break;
                case "Log in":
                    Console.Clear();
                    AnsiConsole.WriteLine("Funcionalidad de inicio de sesión");
                    ScreenController.PauseScreen();
                    break;
                case "Exit":
                    Console.Clear();
                    AnsiConsole.WriteLine("Saliendo del programa...");
                    ScreenController.PauseScreen();
                    return;
                default:
                    break;
            }
        }
    }
}