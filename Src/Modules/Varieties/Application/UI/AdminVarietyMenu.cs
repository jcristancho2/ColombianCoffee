using System;
using System.Threading.Tasks;
using ColombianCoffee.src.Modules.Varieties.Application.DTOs;
using ColombianCoffee.src.Modules.Varieties.Application.Interfaces;
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
            
            try
            {
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
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error al listar variedades: {ex.Message}[/]");
            }
            
            AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private async Task CreateVariety()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold underline]CREAR NUEVA VARIEDAD[/]");

            try
            {
                // Obtener opciones dinámicas de la base de datos
                var speciesOptions = await _varietyService.GetSpeciesOptionsAsync();
                var geneticGroupOptions = await _varietyService.GetGeneticGroupOptionsAsync();
                var lineageOptions = await _varietyService.GetLineageOptionsAsync();
                var altitudeQualityOptions = await _varietyService.GetAltitudeQualityOptionsAsync();

                // Opciones controladas para ENUMs de la BD
                var harvestOptions = new[] { "Sin especificar", "Temprano", "Promedio", "Tardío" };
                var maturationOptions = new[] { "Sin especificar", "Temprano", "Promedio", "Tardío" };
                var nutritionalOptions = new[] { "Sin especificar", "Bajo", "Medio", "Alto" };

                // Prompts controlados para ENUMs
                var harvestSel = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Tiempo de cosecha (opcional):")
                        .AddChoices(harvestOptions));
                var maturationSel = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Tiempo de maduración (opcional):")
                        .AddChoices(maturationOptions));
                var nutritionalSel = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Requerimiento nutricional (opcional):")
                        .AddChoices(nutritionalOptions));

                var newVariety = new VarietyDetailDto
                {
                    Name = AnsiConsole.Ask<string>("Nombre:"),
                    ScientificName = AnsiConsole.Ask<string>("Nombre científico:"),
                    History = AnsiConsole.Ask<string>("Historia (opcional):", ""),
                    SpeciesName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Especie:")
                            .AddChoices(speciesOptions)),
                    GeneticGroupName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Grupo genético:")
                            .AddChoices(geneticGroupOptions)),
                    LineageName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Linaje:")
                            .AddChoices(lineageOptions)),
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
                            .AddChoices(altitudeQualityOptions)),
                    HarvestTime = harvestSel == "Sin especificar" ? null : harvestSel,
                    MaturationTime = maturationSel == "Sin especificar" ? null : maturationSel,
                    NutritionalRequirement = nutritionalSel == "Sin especificar" ? null : nutritionalSel,
                    PlantingDensityValue = AnsiConsole.Ask<decimal?>("Densidad de siembra - valor (opcional):", null),
                    PlantingDensityUnit = AnsiConsole.Ask<string>("Densidad de siembra - unidad (opcional):", ""),
                    AltitudeUnit = AnsiConsole.Ask<string>("Unidad de altitud (opcional):", ""),
                    ImageUrl = AnsiConsole.Ask<string>("URL de imagen (opcional):", "")
                };

                var createdVariety = await _varietyService.CreateVarietyAsync(newVariety);
                
                AnsiConsole.MarkupLine("[bold green]✅ Nueva variedad creada exitosamente:[/]");
                DisplayVarietyDetails(createdVariety);
            }
            catch (Exception ex)
            {
                var root = ex.GetBaseException();
                AnsiConsole.MarkupLine($"[red]Error al crear variedad: {root.Message}[/]");
            }
            
            AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private async Task EditVariety()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold underline]EDITAR VARIEDAD[/]");

            try
            {
                // Primero listar variedades para elegir
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

                // Obtener opciones dinámicas de la base de datos
                var speciesOptions = await _varietyService.GetSpeciesOptionsAsync();
                var geneticGroupOptions = await _varietyService.GetGeneticGroupOptionsAsync();
                var lineageOptions = await _varietyService.GetLineageOptionsAsync();
                var altitudeQualityOptions = await _varietyService.GetAltitudeQualityOptionsAsync();

                // Solicitar nuevos valores
                AnsiConsole.WriteLine("\n[bold]Ingrese los nuevos valores:[/]");

                // Opciones controladas para ENUMs de la BD
                var harvestOptions = new[] { "Sin especificar", "Temprano", "Promedio", "Tardío" };
                var maturationOptions = new[] { "Sin especificar", "Temprano", "Promedio", "Tardío" };
                var nutritionalOptions = new[] { "Sin especificar", "Bajo", "Medio", "Alto" };

                // Prompts controlados para ENUMs
                var harvestSel = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Tiempo de cosecha:")
                        .AddChoices(harvestOptions));
                var maturationSel = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Tiempo de maduración:")
                        .AddChoices(maturationOptions));
                var nutritionalSel = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Requerimiento nutricional:")
                        .AddChoices(nutritionalOptions));

                var updatedVariety = new VarietyDetailDto
                {
                    Id = varietyDetail.Id,
                    Name = AnsiConsole.Ask<string>("Nombre:", varietyDetail.Name),
                    ScientificName = AnsiConsole.Ask<string>("Nombre científico:", varietyDetail.ScientificName),
                    History = AnsiConsole.Ask<string>("Historia:", varietyDetail.History ?? ""),
                    SpeciesName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Especie:")
                            .AddChoices(speciesOptions)),
                    GeneticGroupName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Grupo genético:")
                            .AddChoices(geneticGroupOptions)),
                    LineageName = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Linaje:")
                            .AddChoices(lineageOptions)),
                    PlantHeight = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Porte (altura):")
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
                    MinAltitude = AnsiConsole.Ask<int>("Altitud mínima (msnm):", varietyDetail.MinAltitude),
                    MaxAltitude = AnsiConsole.Ask<int>("Altitud máxima (msnm):", varietyDetail.MaxAltitude),
                    AltitudeQualityLabel = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Calidad de altitud:")
                            .AddChoices(altitudeQualityOptions)),
                    HarvestTime = harvestSel == "Sin especificar" ? null : harvestSel,
                    MaturationTime = maturationSel == "Sin especificar" ? null : maturationSel,
                    NutritionalRequirement = nutritionalSel == "Sin especificar" ? null : nutritionalSel,
                    PlantingDensityValue = AnsiConsole.Ask<decimal?>("Densidad de siembra - valor:", varietyDetail.PlantingDensityValue),
                    PlantingDensityUnit = AnsiConsole.Ask<string>("Densidad de siembra - unidad:", varietyDetail.PlantingDensityUnit ?? ""),
                    AltitudeUnit = AnsiConsole.Ask<string>("Unidad de altitud:", varietyDetail.AltitudeUnit ?? ""),
                    ImageUrl = AnsiConsole.Ask<string>("URL de imagen:", varietyDetail.ImageUrl ?? "")
                };

                var result = await _varietyService.UpdateVarietyAsync(varietyId, updatedVariety);
                
                AnsiConsole.MarkupLine("[bold green]✅ Variedad actualizada exitosamente:[/]");
                DisplayVarietyDetails(result);
            }
            catch (Exception ex)
            {
                var root = ex.GetBaseException();
                AnsiConsole.MarkupLine($"[red]Error al editar variedad: {root.Message}[/]");
            }
            
            AnsiConsole.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private async Task DeleteVariety()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold underline]ELIMINAR VARIEDAD[/]");

            try
            {
                // Primero listar variedades para elegir
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
                    var result = await _varietyService.DeleteVarietyAsync(varietyId);
                    if (result)
                    {
                        AnsiConsole.MarkupLine($"[green]✅ Variedad {varietyDetail.Name} eliminada correctamente.[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]❌ Error al eliminar la variedad.[/]");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[yellow]Operación cancelada. No se eliminó ninguna variedad.[/]");
                }
            }
            catch (Exception ex)
            {
                var root = ex.GetBaseException();
                AnsiConsole.MarkupLine($"[red]Error al eliminar variedad: {root.Message}[/]");
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