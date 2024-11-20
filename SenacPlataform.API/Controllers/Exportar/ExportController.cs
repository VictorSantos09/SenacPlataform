using Microsoft.AspNetCore.Mvc;
using QuickKit.Shared.Export;
using QuickKit.Shared.Generator.Csv;
using QuickKit.Shared.Generator.Excel;
using System.Text;

namespace SenacPlataform.API.Controllers.Exportar
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class ExportController : ControllerBase
    {
        [HttpPost("csv")]
        public IActionResult ExportToCsv([FromBody] ExportRequest exportRequest)
        {
            var csvData = CsvGenerator.Generate(exportRequest.Data, exportRequest.Columns);
            return File(new UTF8Encoding().GetBytes(csvData), "text/csv", $"{exportRequest.FileName}.csv");
        }

        [HttpPost("excel")]
        public IActionResult ExportToExcel([FromBody] ExportRequest exportRequest)
        {
            var result = ExcelGenerator.Generate(exportRequest.Data, exportRequest.Columns);
            return new FileStreamResult(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"{exportRequest.FileName}.xlsx"
            };
        }
    }
}
