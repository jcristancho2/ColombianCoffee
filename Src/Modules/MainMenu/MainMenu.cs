using ColombianCoffee.src.Modules.Varieties.Application.Services;
using ColombianCoffee.src.Modules.Varieties.Application.UI;
using ColombianCoffee.src.Modules.Varieties.Infrastructure;
using ColombianCoffee.Src.Modules.Auth.Application.Services;
using ColombianCoffee.Src.Modules.Auth.Application.UI;
using ColombianCoffee.Src.Modules.Auth.Infraestructure.Repositories;
using ColombianCoffee.Src.Modules.PDFExport.Application.Services;
using ColombianCoffee.Src.Shared.Contexts;
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
        var pdfGeneratorService = new PdfGeneratorService();
        _varietyUI = new VarietyUI(varietyService, pdfGeneratorService);
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

            // Debug: Mostrar estado de la sesión
            if (SessionManager.CurrentUser != null)
            {
                AnsiConsole.MarkupLine($"[yellow]DEBUG: Usuario autenticado: {SessionManager.CurrentUser.Username} ({SessionManager.CurrentUser.Role})[/]");
                await ShowAuthenticatedMenu();
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]DEBUG: No hay usuario autenticado[/]");
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
                if (user != null)
                {
                    Console.Clear();
                    if (user.Role == UserRole.admin)
                    {
                        // Panel de admin por implementar
                        AnsiConsole.MarkupLine("[yellow]Panel de admin por implementar[/]");
                        Console.WriteLine("Presione ENTER para continuar...");
                        Console.ReadLine();
                    }
                    else
                    {
                        await _varietyUI.Show(); // Los usuarios normales van a VarietyUI
                    }
                    return; // Salir del método para que el bucle principal detecte el usuario autenticado
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
        AnsiConsole.MarkupLine("[yellow]DEBUG: Ejecutando ShowAuthenticatedMenu[/]");

        var choices = user.Role == UserRole.admin
            ? new[] { "Admin Panel (Por implementar)", "Log out" }
            : new[] { "Search Varieties", "Log out" };

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option")
                .AddChoices(choices)
        );

        switch (selection)
        {
            case "Search Varieties":
                Console.Clear();
                await _varietyUI.Show();
                break;

            case "Admin Panel (Por implementar)":
                Console.Clear();
                AnsiConsole.MarkupLine("[yellow]Panel de admin por implementar[/]");
                Console.WriteLine("Presione ENTER para continuar...");
                Console.ReadLine();
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