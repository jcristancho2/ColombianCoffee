using ColombianCoffee.Src.Modules.Varieties.Application.Services;
using ColombianCoffee.Src.Modules.Varieties.Application.UI;
using ColombianCoffee.Src.Modules.Varieties.Infrastructure;
using ColombianCoffee.Src.Modules.Auth.Application.Services;
using ColombianCoffee.Src.Modules.Auth.Application.UI;
using ColombianCoffee.Src.Modules.Auth.Infraestructure.Repositories;
using ColombianCoffee.Src.Modules.PDFExport.Application.Services;
using ColombianCoffee.Src.Modules.PDFExport.Application.Interfaces;
using ColombianCoffee.Src.Shared.Contexts;
using Spectre.Console;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;

namespace ColombianCoffee.Src.Modules.MainMenu;

public class MainMenu
{
    private readonly AppDbContext _dbContext;
    private readonly AuthMenu _authMenu;
    private readonly VarietyUI _varietyUI;
    private readonly AdminVarietyMenu _adminVarietyMenu;

    public MainMenu(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        var userRepository = new UserRepository(_dbContext);
        var authService = new AuthService(userRepository);
        _authMenu = new AuthMenu(authService);

        var varietyRepository = new VarietyRepository(_dbContext);
        var varietyService = new VarietyService(varietyRepository);
        var pdfGenerator = new PdfGeneratorService();
        _varietyUI = new VarietyUI(varietyService, pdfGenerator);
        _adminVarietyMenu = new AdminVarietyMenu(varietyService);
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
                AnsiConsole.MarkupLine($"[green]Usuario autenticado:[/] {SessionManager.CurrentUser.Username} ([grey]{SessionManager.CurrentUser.Role}[/])");
                await ShowAuthenticatedMenu();
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]No hay usuario autenticado.[/]");
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
                var user = await _authMenu.Login(); // Login -> devuelve User
                if (user != null)
                {
                    Console.Clear();
                    // Volver al bucle principal para mostrar el menú autenticado según rol
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
        AnsiConsole.MarkupLine($"[bold green]Bienvenido, {user.Username}[/] ([grey]{user.Role}[/])");

        var choices = user.Role == UserRole.admin
            ? new[] { "Administrar Variedades", "Explorar Variedades", "Cerrar sesión" }
            : new[] { "Explorar Variedades", "Cerrar sesión" };

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Seleccione una opción")
                .AddChoices(choices));

        switch (selection)
        {
            case "Administrar Variedades":
                Console.Clear();
                await _adminVarietyMenu.ShowMenu(user);
                break;

            case "Explorar Variedades":
                Console.Clear();
                await _varietyUI.Show();
                break;

            case "Cerrar sesión":
                SessionManager.Logout();
                AnsiConsole.MarkupLine("[yellow]Sesión cerrada.[/]");
                Console.WriteLine("Presione ENTER para continuar...");
                Console.ReadLine();
                break;
        }
    }
}