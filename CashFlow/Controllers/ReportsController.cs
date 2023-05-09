using CashFlow.Api.Exceptions;
using CashFlow.Api.Filters;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("dailyBalance")]
        public async Task<ActionResult<Report>> GetDailyBalanceReport(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ValidationException("Invalid date range. The start date cannot be greater than the end date.");
            }

            return Ok(await _reportService.GenerateDailyBalanceReportAsync(startDate, endDate));
        }
    }
}
