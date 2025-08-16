using ColombianCoffee.src.Modules.Varieties.Application.Services;
using ColombianCoffee.src.Modules.Varieties.Application.UI;
using ColombianCoffee.src.Modules.Varieties.Infrastructure;
using ColombianCoffee.Src.Modules.Auth.Application.Services;
using ColombianCoffee.Src.Modules.Auth.Application.UI;
using ColombianCoffee.Src.Modules.Auth.Infraestructure.Repositories;
using ColombianCoffee.Src.Shared.Contexts;
using ColombianCoffee.Src.Shared.Helpers;
using ColombianCoffee.Src.Shared.Utils;
using Spectre.Console;

namespace ColombianCoffee.Src.Modules.MainMenu;

public class MainMenu
{
    private readonly AppDbContext _dbContext;
    private readonly AuthMenu _authMenu;
    private readonly VarietyUI _varietyUI;

    public MainMenu(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        var userRepository = new UserRepository(DbContextFactory.Create());
        var authService = new AuthService(userRepository);
        _authMenu = new AuthMenu(authService);

        var varietyRepository = new VarietyRepository(DbContextFactory.Create());
        var varietyService = new VarietyService(varietyRepository);
        _varietyUI = new VarietyUI(varietyService);
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

            if (SessionManager.CurrentUser != null)
            {
                await ShowAuthenticatedMenu();
            }
            else
            {
                await ShowGuestMenu();
            }
        }
    }

    private async Task ShowGuestMenu()
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option")
                .AddChoices(new[] { "Sign up", "Log in", "Exit" })
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
                Environment.Exit(0);
                break;
        }
    }

    private async Task ShowAuthenticatedMenu()
       {
        var user = SessionManager.CurrentUser!;
        AnsiConsole.MarkupLine($"[bold green]Bienvenido, {user.Username} ({user.Role})[/]");

        var choices = user.Role == UserRole.Admin
            ? new[] { "Manage Varieties", "Log out" }
            : new[] { "Log out" };

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option")
                .AddChoices(choices)
        );

        switch (selection)
        {
            case "Manage Varieties":
                try
                {
                    SessionManager.validateRole(UserRole.Admin); // Validar rol
                    Console.Clear();
                    AnsiConsole.WriteLine("Gestión de variedades de café");
                    await _varietyUI.Show();
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
                }
                ScreenController.PauseScreen();
                break;

            case "Log out":
                SessionManager.Logout();
                AnsiConsole.MarkupLine("[yellow]Sesión cerrada.[/]");
                ScreenController.PauseScreen();
                break;
        }
    }
}