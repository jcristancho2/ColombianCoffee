using System.Threading.Tasks;
using ColombianCoffee.Src.Modules.Varieties.Application.DTOs;

namespace ColombianCoffee.Src.Modules.PDFExport.Application.Interfaces
{
    public interface IPdfGenerator
    {
        Task GenerateCoffeeVarietyDetailPdf(VarietyDetailDto varietyDetail, string outputPath);
    }
}