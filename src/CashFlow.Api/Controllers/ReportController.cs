using System.Net.Mime;
using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        [HttpGet]
        [Route("excel")]
        [ProducesResponseType<FileContentResult>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(
            Summary = "Get an excel report",
            Description = "Get an excel report with all expenses registered in the system",
            OperationId = "GetExcelReport",
            Tags = ["Reports"]
        )]
        public async Task<IActionResult> GetExcelReport([FromHeader] DateOnly month, [FromServices] IGenerateExpensesReportExcelUseCase useCase)
        {
            var fileBytes = await useCase.Execute(month);

            if (fileBytes.Length != 0)
            {
                return File(fileBytes, MediaTypeNames.Application.Octet, "report.xlsx");
            }

            return NoContent();
        }
    }
}
