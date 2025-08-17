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

        var userRepository = new UserRepository(_dbContext);
        var authService = new AuthService(userRepository);
        _authMenu = new AuthMenu(authService);

        var varietyRepository = new VarietyRepository(_dbContext);
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
                await _authMenu.Register(); // Registro -> vuelve al menú principal
                break;

            case "Log in":
                Console.Clear();
                var user = await _authMenu.Login(); // Login -> devuelve User?
                if (user != null && user.Role == UserRole.admin)
                {
                    Console.Clear();
                    await _varietyUI.Show(); // Abre VarietyUI después de login
                }
                break;

            case "Exit":
                Console.Clear();
                AnsiConsole.WriteLine("Saliendo del programa...");
                Environment.Exit(0);
                break;
        }
    }

    private async Task ShowAuthenticatedMenu()
    {
        var user = SessionManager.CurrentUser!;
        AnsiConsole.MarkupLine($"[bold green]Bienvenido, {user.Username} ({user.Role})[/]");

        var choices = user.Role == UserRole.admin
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
                Console.Clear();
                await _varietyUI.Show();
                break;

            case "Log out":
                SessionManager.Logout();
                AnsiConsole.MarkupLine("[yellow]Sesión cerrada.[/]");
                Console.WriteLine("Presione ENTER para continuar...");
                Console.ReadLine();
                break;
        }
    }
}