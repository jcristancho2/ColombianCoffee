using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColombianCoffee.src.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.Src.Modules.PDFExport.Application.Interfaces
{
    public interface IPdfGenerator
    {
        Task GenerateCoffeeVarietyPdf(Variety variety, string outputPath);
        Task GenerateCoffeeVarietiesReportPdf(IEnumerable<Variety> varieties, string outputPath);
    }
}