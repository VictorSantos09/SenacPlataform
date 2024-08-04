using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenacPlataform.Shared.Controllers;

namespace BancoTalentos.API.Controllers
{
    [Route("excel")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [GetAll]
        public async Task<IActionResult> teste()
        {
            string path = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads\\Teste2.xlsx";

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sample Sheet");
                worksheet.Cell("A1").Value = "Teste!";
                worksheet.Cell("A2").Value = "Teste 2";
                workbook.SaveAs(path);

                


            }
            

            return Ok("Sucesso truta");
        }


    }
}
