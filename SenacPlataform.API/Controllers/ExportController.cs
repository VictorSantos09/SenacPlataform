using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;

namespace RadzenBlazorDemos
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class ExportController : ControllerBase
    {
        // Export CSV via POST com os dados visíveis e colunas
        [HttpPost("csv")]
        public IActionResult ExportCsv([FromBody] ExportRequest exportRequest)
        {
            var csvData = GenerateCsv(exportRequest.Data, exportRequest.Columns);
            return File(new UTF8Encoding().GetBytes(csvData), "text/csv", "export.csv");
        }

        // Export Excel via POST com dados visíveis e colunas
        [HttpPost("excel")]
        public IActionResult ExportToExcel([FromBody] ExportRequest exportRequest)
        {
            var result = ToExcel(exportRequest.Data, exportRequest.Columns);
            return result;
        }

        // Classe de requisição com colunas e dados visíveis
        public class ExportRequest
        {
            public string[] Columns { get; set; }
            public IEnumerable<IDictionary<string, object>> Data { get; set; }
        }

        // Método para gerar CSV
        private static string GenerateCsv(IEnumerable<IDictionary<string, object>> data, string[] columns)
        {
            var sb = new StringBuilder();

            sb.AppendLine(string.Join(",", columns));

            Dictionary<string, object> caseInsensitiveItem;
            foreach (var item in data)
            {
                caseInsensitiveItem = new Dictionary<string, object>(item, StringComparer.OrdinalIgnoreCase);

                var row = columns
                    .Select(col => caseInsensitiveItem.TryGetValue(col, out var value) ? value?.ToString() : "")
                    .ToArray();

                sb.AppendLine(string.Join(",", row));
            }

            return sb.ToString();
        }


        // Método para gerar o arquivo Excel com os dados
        private static FileStreamResult ToExcel(IEnumerable<IDictionary<string, object>> data, string[] columns)
        {
            var stream = new MemoryStream();
            using (var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
                sheets.Append(sheet);

                var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Cabeçalho das colunas
                var headerRow = new Row();
                foreach (var column in columns)
                {
                    headerRow.Append(new Cell
                    {
                        CellValue = new CellValue(column),
                        DataType = CellValues.String
                    });
                }
                sheetData.AppendChild(headerRow);

                // Dados das linhas
                foreach (var item in data)
                {
                    var row = new Row();
                    foreach (var column in columns)
                    {
                        var cellValue = item.ContainsKey(column.ToLower()) ? item[column.ToLower()]?.ToString() ?? "" : "";
                        row.Append(new Cell
                        {
                            CellValue = new CellValue(cellValue),
                            DataType = CellValues.String
                        });
                    }
                    sheetData.AppendChild(row);
                }
            }

            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "export.xlsx"
            };
        }
    }
}
