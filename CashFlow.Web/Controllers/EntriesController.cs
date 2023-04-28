using CashFlow.Web.Models;
using CashFlow.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Web.Controllers
{
    public class EntriesController : Controller
    {
        private readonly CashFlowApiService _cashFlowApiService;

        public EntriesController(CashFlowApiService cashFlowApiService)
        {
            _cashFlowApiService = cashFlowApiService;
        }

        // GET: Entries
        public async Task<IActionResult> Index()
        {
            var entries = await _cashFlowApiService.GetEntriesAsync();
            return View(entries);
        }

        // GET: Entries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Amount,Type,Description")] EntryViewModel entry)
        {
            if (ModelState.IsValid)
            {
                await _cashFlowApiService.CreateEntryAsync(entry);
                return RedirectToAction(nameof(Index));
            }

            return View(entry);
        }

        // GET: Entries/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var entry = await _cashFlowApiService.GetEntryByIdAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // POST: Entries/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Date,Amount,Type,Description")] EntryViewModel entry)
        {
            if (id != entry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _cashFlowApiService.UpdateEntryAsync(id, entry);
                return RedirectToAction(nameof(Index));
            }

            return View(entry);
        }

        // GET: Entries/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var entry = await _cashFlowApiService.GetEntryByIdAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // POST: Entries/Delete/{id}
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _cashFlowApiService.DeleteEntryAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Report
        public async Task<IActionResult> Report()
        {
            var report = await _cashFlowApiService.GetDailyBalanceReportAsync(DateTime.Now);
            return View(report);
        }
    }

}
