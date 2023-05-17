using CashFlow.Web.Models;
using CashFlow.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly CashFlowApiService _cashFlowApiService;

        public ReportsController(CashFlowApiService cashFlowApiService)
        {
            _cashFlowApiService = cashFlowApiService;
        }

        // GET: Report
        public async Task<IActionResult> Report(DateTime startDate)
        {
            if (startDate == DateTime.MinValue) startDate = DateTime.Now;
            var report = await _cashFlowApiService.GetDailyBalanceReportAsync(startDate);
            return View(report);
        }
    }
}
