using System.Numerics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GenerarPdf
{
    public class Document
    {
        static Document()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        //Ejemplo de método para guardar un PDF usando QuestPDF
        public static void GenerateCoffeePdf()
        {
            var document = new MyCoffeeDocument(); //Implementacion del IDocument
            document.ShowInCompanion();
            Console.WriteLine("PDF guardado en MyCoffeeDocument");

        }
    }
    internal class MyCoffeeDocument : IDocument
    {
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public void Compose(IDocumentContainer container)
        {
            container.Page(static page =>
            {
                string backgroundPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\src\img\Background.png");
                page.Background().Image(backgroundPath).FitArea();
                page.Header().Element(Container_Header);
                page.Content().Element(Container_Body);
                page.Footer().Element(Container_Footer);
            });
        }
        private static void Container_Header(IContainer container)
        {
            container
            .PaddingLeft(40)
            .PaddingRight(40)
            .PaddingTop(40)
            .PaddingBottom(30)
            .Row(row =>
                {
                    string imgPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\src\img\coffee.webp");

                    row.RelativeItem()
                        .PaddingTop(10)

                            .Column(row =>
                            {
                                //Nombre comun
                                row.Item()
                                    .AlignLeft()
                                    .Text("INIFAB 8")
                                    .FontSize(21)
                                    .Bold();
                                //Nombre cientifico
                                row.Item()
                                    .AlignLeft()
                                    .PaddingTop(2)
                                    .Text("INIFAB 8")
                                    .FontSize(16)
                                    .Italic();
                                //Descripcion
                                row.Item()
                                    .AlignLeft()
                                    .PaddingTop(2)
                                    .Text("Tall plants with large numerous leaves and fruits; highest yielding clone for the conditions of the coast of Chiapas, Mexico.")
                                    .FontSize(13)
                                    .Light();
                            });
                    row.ConstantItem(150)
                        .AlignRight()
                        .Width(100)
                        .Height(100)
                        .CornerRadius(50)
                        .Border(1)
                        .BorderColor("#8B4513")
                        .Image(imgPath)
                        .FitHeight();

                });
        }

        private static void Container_Body(IContainer container)
        {

            container
                .PaddingLeft(40)
                .PaddingRight(40)
                .Table(static table =>
                {
                    table.ColumnsDefinition(Columns =>
                    {
                        Columns.ConstantColumn(155);
                        Columns.ConstantColumn(355);
                    });
                    //                foreach (var i in Enumerable.Range(0, 6))
                    //                {
                    //                    table.Cell().Padding(10).Text("asdfghjasdfg");
                    //                    table.Cell().Padding(8).Text("asdfasdfghjasdfghjkasdfghjertyuiodfghj");
                    //                    };
                    //                });
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Agronomics").FontSize(16).Bold();
                        static IContainer CellStyle(IContainer container)
                        {
                            return container
                                .Background("#10FFFFFF")
                                .PaddingBottom(10);
                        }
                    });

                    foreach (var i in Enumerable.Range(0, 7))
                    {
                        table.Cell()
                            .Element(c => CellStyle(c, i))
                            .Text("info ~ info ~")
                            .FontSize(13);
                        table.Cell()
                            .Element(c => CellStyle(c, i))
                            .Text("info ~ info ~")
                            .FontSize(13);

                    }

                    static IContainer CellStyle(IContainer container, int i)
                    {
                        var backgroundColor = i % 2 == 0
                            ? "#608B4513"
                            : "#80FFFFFF";
                        return container
                        .Background(backgroundColor)
                        .PaddingVertical(3)
                        .PaddingHorizontal(10);
                    }

                }

            );
            
        }

        private static void Container_Footer(IContainer container)
        {
            container
                .Padding(40)
                .Row(row =>
                {
                    string imgPath2 = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\src\img\BrEd.png");
                    row.ConstantItem(45)
                        .AlignLeft()
                        .PaddingTop(5)
                        .Image(imgPath2);

                    row.RelativeItem()
                        .Padding(10)
                        .PaddingLeft(6)

                            .Column(Footer_row =>
                            {
                                //Editor's name
                                Footer_row.Item()
                                    .AlignLeft()
                                    .Text("Elaborado por: Breakline Education")
                                    .FontSize(12)
                                    .Light();

                                //Date
                                Footer_row.Item()
                                    .AlignLeft()
                                    .Text(Placeholders.DateTime())
                                    .FontSize(12)
                                    .Light();;
                            });

                });
        }
    }
}




