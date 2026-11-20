using ClosedXML.Excel;
namespace Lab13.Api;
public class ExcelExamples
{
    public string OutputDirectory { get; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "Lab13",
        "Excel");

    public ExcelExamples()
    {
        Directory.CreateDirectory(OutputDirectory);
    }

    // Parte 2: crear un archivo Excel.
    public void FirstExample()
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Hoja1");

        worksheet.Cell(1, 1).Value = "Nombre";
        worksheet.Cell(1, 2).Value = "Edad";
        worksheet.Cell(2, 1).Value = "Juan";
        worksheet.Cell(2, 2).Value = 28;

        workbook.SaveAs(Path.Combine(OutputDirectory, "archivo.xlsx"));
    }

    // Parte 3: abrir el archivo existente y modificar B2.
    public void SecondExample()
    {
        using var workbook = new XLWorkbook(
            Path.Combine(OutputDirectory, "archivo.xlsx"));

        var worksheet = workbook.Worksheet(1);
        worksheet.Cell(2, 2).Value = 30;

        workbook.Save();
    }

    // Parte 4: crear una tabla en el rango A1:C3.
    public void ThirdExample()
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Datos");

        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Nombre";
        worksheet.Cell(1, 3).Value = "Edad";

        worksheet.Cell(2, 1).Value = 1;
        worksheet.Cell(2, 2).Value = "Juan";
        worksheet.Cell(2, 3).Value = 28;

        worksheet.Cell(3, 1).Value = 2;
        worksheet.Cell(3, 2).Value = "María";
        worksheet.Cell(3, 3).Value = 34;

        worksheet.Range("A1:C3").CreateTable();

        workbook.SaveAs(Path.Combine(OutputDirectory, "tabla.xlsx"));
    }

    // Parte 5: aplicar formato a la primera fila.
    public void FourthExample()
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Estilos");

        var headerRow = worksheet.Row(1);
        headerRow.Style.Font.Bold = true;
        headerRow.Style.Fill.BackgroundColor = XLColor.Cyan;
        headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Nombre";
        worksheet.Cell(1, 3).Value = "Edad";

        worksheet.Cell(2, 1).Value = 1;
        worksheet.Cell(2, 2).Value = "Juan";
        worksheet.Cell(2, 3).Value = 28;

        workbook.SaveAs(
            Path.Combine(OutputDirectory, "archivo_con_estilos.xlsx"));
    }
}