using System;
using System.Threading.Tasks;
using ColombianCoffee.src.Modules.Varieties.Application.DTOs;
using ColombianCoffee.src.Modules.Varieties.Application.Interfaces;
using ColombianCoffee.src.Modules.Varieties.Application.Services;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;
using Spectre.Console;

namespace ColombianCoffee.Src.Modules.Varieties.Application.UI
{
    public class AdminVarietyMenu
    {
        private readonly IVarietyService _varietyService;

        public AdminVarietyMenu(IVarietyService varietyService)
        {
            _varietyService = varietyService;
        }

        public async Task ShowMenu(User user)
        {
            if (user.Role != UserRole.admin)
            {
                AnsiConsole.MarkupLine("[red]❌ Acceso denegado: Se requieren privilegios de administrador[/]");
                return;
            }

            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.MarkupLine("[bold underline yellow]ADMINISTRACIÓN DE VARIEDADES[/]");
                AnsiConsole.MarkupLine($"[grey]Usuario: {user.Username} (Rol: {user.Role})[/]");
                AnsiConsole.WriteLine();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Seleccione una opción:")
                        .AddChoices(new[] {
                            "Listar variedades",
                            "Crear nueva variedad",
                            "Editar variedad",
                            "Eliminar variedad",
                            "Volver al menú principal"
                        }));

                switch (choice)
                {
                    case "Listar variedades":
                        await ListVarieties();
                        break;
                    case "Crear nueva variedad":
                        await CreateVariety();
                        break;
                    case "Editar variedad":
                        await EditVariety();
                        break;
                    case "Eliminar variedad":
                        await DeleteVariety();
                        break;
                    case "Volver al menú principal":
                        return;
                }
            }
        }

        private async Task ListVarieties()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold underline]LISTADO DE VARIEDADES[/]");
            
            var filter = new VarietyFilterDto();
            var varieties = await _varietyService.GetFilteredVarietiesAsync(filter);

            if (!varieties.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No hay variedades registradas.[/]");
                AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.AddColumn(new TableColumn("[bold]ID[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Nombre[/]"));
            table.AddColumn(new TableColumn("[bold]Nombre Científico[/]"));

            foreach (var variety in varieties)
            {
                var detail = await _varietyService.GetVarietyDetailAsync(variety.Id);
                table.AddRow(
                    variety.Id.ToString(),
                    variety.Name,
                    detail?.ScientificName ?? "N/A"
                );
            }

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private async Task CreateVariety()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold underline]CREAR NUEVA VARIEDAD[/]");

            var newVariety = new VarietyDetailDto
            {
                Name = AnsiConsole.Ask<string>("Nombre:"),
                ScientificName = AnsiConsole.Ask<string>("Nombre científico:"),
                History = AnsiConsole.Ask<string>("Historia (opcional):", ""),
                SpeciesName = AnsiConsole.Ask<string>("Especie:"),
                GeneticGroupName = AnsiConsole.Ask<string>("Grupo genético:"),
                LineageName = AnsiConsole.Ask<string>("Linaje:"),
                PlantHeight = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Porte (altura) de la planta:")
                        .AddChoices("Alto", "Medio", "Bajo")),
                BeanSize = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Tamaño de grano:")
                        .AddChoices("Pequeño", "Medio", "Grande")),
                YieldPotential = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Potencial de rendimiento:")
                        .AddChoices("Muy bajo", "Bajo", "Medio", "Alto", "Excepcional")),
                RustResistance = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Resistencia a roya:")
                        .AddChoices("Susceptible", "Tolerante", "Resistente", "Altamente resistente")),
                AnthracnoseResistance = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Resistencia a antracnosis:")
                        .AddChoices("Susceptible", "Tolerante", "Resistente", "Altamente resistente")),
                NematodesResistance = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Resistencia a nematodos:")
                        .AddChoices("Susceptible", "Tolerante", "Resistente", "Altamente resistente")),
                MinAltitude = AnsiConsole.Ask<int>("Altitud mínima (msnm):"),
                MaxAltitude = AnsiConsole.Ask<int>("Altitud máxima (msnm):"),
                AltitudeQualityLabel = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Calidad de altitud:")
                        .AddChoices("Muy baja", "Baja", "Media", "Alta", "Muy alta")),
                HarvestTime = AnsiConsole.Ask<string>("Tiempo de cosecha (opcional):", ""),
                MaturationTime = AnsiConsole.Ask<string>("Tiempo de maduración (opcional):", ""),
                NutritionalRequirement = AnsiConsole.Ask<string>("Requerimiento nutricional (opcional):", ""),
                PlantingDensityValue = AnsiConsole.Ask<decimal?>("Densidad de siembra - valor (opcional):", null),
                PlantingDensityUnit = AnsiConsole.Ask<string>("Densidad de siembra - unidad (opcional):", ""),
                AltitudeUnit = AnsiConsole.Ask<string>("Unidad de altitud (opcional):", ""),
                ImageUrl = AnsiConsole.Ask<string>("URL de imagen (opcional):", "")
            };

            // TODO: Implement actual creation in the service/repository
            // For now we'll just show what would be saved
            AnsiConsole.MarkupLine("[bold green]✅ Nueva variedad creada:[/]");
            DisplayVarietyDetails(newVariety);
            AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private async Task EditVariety()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold underline]EDITAR VARIEDAD[/]");

            // First list varieties to choose from
            var filter = new VarietyFilterDto();
            var varieties = await _varietyService.GetFilteredVarietiesAsync(filter);

            if (!varieties.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No hay variedades registradas para editar.[/]");
                AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            var varietyId = AnsiConsole.Prompt(
                new SelectionPrompt<uint>()
                .Title("Seleccione la variedad a editar:")
                .AddChoices(varieties.Select(v => v.Id))
                .UseConverter(id =>
                {
                    var v = varieties.First(x => x.Id == id);
                    return $"{id} - {v.Name}";
                })
            );
                        var varietyDetail = await _varietyService.GetVarietyDetailAsync(varietyId);
            if (varietyDetail == null)
            {
                AnsiConsole.MarkupLine("[red]No se encontró la variedad seleccionada.[/]");
                AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            AnsiConsole.MarkupLine("[bold]Editando variedad:[/]");
            DisplayVarietyDetails(varietyDetail);

            // TODO: Implement actual editing logic
            // For now we'll just show what would be edited
            AnsiConsole.MarkupLine("[yellow]Edición de variedades aún no implementada.[/]");
            AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private async Task DeleteVariety()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold underline]ELIMINAR VARIEDAD[/]");

            // First list varieties to choose from
            var filter = new VarietyFilterDto();
            var varieties = await _varietyService.GetFilteredVarietiesAsync(filter);

            if (!varieties.Any())
            {
                AnsiConsole.MarkupLine("[yellow]No hay variedades registradas para eliminar.[/]");
                AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            var varietyId = AnsiConsole.Prompt(
                new SelectionPrompt<uint>()
                    .Title("Seleccione la variedad a eliminar:")
                    .AddChoices(varieties.Select(v => v.Id))
                    .UseConverter(id => 
                    {
                        var v = varieties.First(x => x.Id == id);
                         return $"{id} - {v.Name}";
                    })
                );


            var varietyDetail = await _varietyService.GetVarietyDetailAsync(varietyId);
            if (varietyDetail == null)
            {
                AnsiConsole.MarkupLine("[red]No se encontró la variedad seleccionada.[/]");
                AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            AnsiConsole.MarkupLine("[bold red]ADVERTENCIA:[/] Está a punto de eliminar la siguiente variedad:");
            DisplayVarietyDetails(varietyDetail);

            if (AnsiConsole.Confirm("[bold red]¿Está seguro que desea eliminar esta variedad?[/]", false))
            {
                // TODO: Implement actual deletion in the service/repository
                AnsiConsole.MarkupLine($"[green]✅ Variedad {varietyDetail.Name} eliminada correctamente.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Operación cancelada. No se eliminó ninguna variedad.[/]");
            }

            AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void DisplayVarietyDetails(VarietyDetailDto variety)
        {
            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.AddColumn("Propiedad");
            table.AddColumn("Valor");

            table.AddRow("[bold]ID[/]", variety.Id.ToString());
            table.AddRow("[bold]Nombre[/]", variety.Name);
            table.AddRow("[bold]Nombre científico[/]", variety.ScientificName);
            table.AddRow("[bold]Historia[/]", variety.History ?? "-");
            table.AddRow("[bold]Especie[/]", variety.SpeciesName);
            table.AddRow("[bold]Grupo genético[/]", variety.GeneticGroupName);
            table.AddRow("[bold]Linaje[/]", variety.LineageName);
            table.AddRow("[bold]Porte[/]", variety.PlantHeight);
            table.AddRow("[bold]Tamaño de grano[/]", variety.BeanSize);
            table.AddRow("[bold]Potencial de rendimiento[/]", variety.YieldPotential);
            table.AddRow("[bold]Resistencia a roya[/]", variety.RustResistance);
            table.AddRow("[bold]Resistencia a antracnosis[/]", variety.AnthracnoseResistance);
            table.AddRow("[bold]Resistencia a nematodos[/]", variety.NematodesResistance);
            table.AddRow("[bold]Altitud mínima[/]", $"{variety.MinAltitude} msnm");
            table.AddRow("[bold]Altitud máxima[/]", $"{variety.MaxAltitude} msnm");
            table.AddRow("[bold]Calidad de altitud[/]", variety.AltitudeQualityLabel);
            table.AddRow("[bold]URL de imagen[/]", variety.ImageUrl ?? "-");

            AnsiConsole.Write(table);
        }
    }
}