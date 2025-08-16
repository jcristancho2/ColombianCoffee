using ColombianCoffee.Src.Shared.Contexts;
using ColombianCoffee.Src.Shared.Utils;
using ColombianCoffee.Src.Modules.Auth.Application.Services;
using ColombianCoffee.Src.Modules.Auth.Application.UI;
using ColombianCoffee.Src.Modules.Auth.Infraestructure.Repositories;
using ColombianCoffee.Src.Modules.Auth.Application.Interfaces;
using Spectre.Console;


namespace ColombianCoffee.Src.Modules.MainMenu;

public class MainMenu
{
    private readonly AppDbContext _dbContext;
    private readonly AuthMenu _authMenu;

    public MainMenu(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        IUserRepository userRepo = new UserRepository(_dbContext);
        var authService = new AuthService(userRepo);
        _authMenu = new AuthMenu(authService);
    }

    public async Task Show()
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
                    await _authMenu.Register();
                    ScreenController.PauseScreen();
                    break;
                case "Log in":
                    Console.Clear();
                    AnsiConsole.WriteLine("Funcionalidad de inicio de sesión");
                    await _authMenu.Login();
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