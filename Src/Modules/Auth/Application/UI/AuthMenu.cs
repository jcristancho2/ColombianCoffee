using System;
using System.Threading.Tasks;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;
using ColombianCoffee.Src.Modules.Auth.Application.Services;
using Spectre.Console;

namespace ColombianCoffee.Src.Modules.Auth.Application.UI
{
    public class AuthMenu
    {
        private readonly AuthService _authService;

        public AuthMenu(AuthService authService)
        {
            _authService = authService;
        }

        public async Task Register()
        {
            var username = AnsiConsole.Ask<string>("Ingrese [green]Username[/]:");
            var email = AnsiConsole.Ask<string>("Ingrese [green]Email[/]:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Ingrese [green]Password[/]:")
                    .PromptStyle("red")
                    .Secret()
            );
            var roleInput = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Seleccione el [green]Rol[/]:")
                    .AddChoices("Admin", "Usuario")
            );

            var role = Enum.Parse<UserRole>(roleInput, true);

            try
            {
                await _authService.RegisterAsync(username, email, password, role);
                AnsiConsole.MarkupLine("[bold green]✅ Usuario registrado correctamente![/]");
                AnsiConsole.MarkupLine($"[yellow]Usuario:[/] {username} | [yellow]Email:[/] {email}");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
            }

            AnsiConsole.Prompt(new TextPrompt<string>("Presione ENTER para continuar..."));
        }

        public async Task Login()
        {
            var usernameOrEmail = AnsiConsole.Ask<string>("Ingrese [green]Username o Email[/]:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Ingrese [green]Password[/]:")
                    .PromptStyle("red")
                    .Secret()
            );

            try
            {
                var user = await _authService.LoginAsync(usernameOrEmail, password);
                SessionManager.Login(user);
                AnsiConsole.MarkupLine($"[bold green]Bienvenido {user.Username}![/] [yellow]Rol:[/] {user.Role}");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
            }

            AnsiConsole.Prompt(new TextPrompt<string>("Presione ENTER para continuar..."));
        }
    }
}
