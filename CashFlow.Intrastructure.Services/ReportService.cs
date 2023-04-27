using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CashFlow.Domain.Entities.Entry;

namespace CashFlow.Intrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly IEntryRepository _entryRepository;

        public ReportService(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<Report> GenerateDailyBalanceReportAsync(DateTime startDate, DateTime endDate)
        {
            var entries = await _entryRepository.GetAllEntriesAsync();
            var dailyBalances = new List<DailyBalance>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var dailyDebits = entries
                    .Where(e => e.Type == EntryType.Debit && e.Date.Date == date.Date)
                    .Sum(e => e.Amount);

                var dailyCredits = entries
                    .Where(e => e.Type == EntryType.Credit && e.Date.Date == date.Date)
                    .Sum(e => e.Amount);

                dailyBalances.Add(new DailyBalance(date, dailyDebits, dailyCredits));
            }

            return new Report(startDate, endDate, dailyBalances);
        }
    }
}
