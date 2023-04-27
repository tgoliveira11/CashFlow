using CashFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Services
{
    public interface IReportService
    {
        Task<Report> GenerateDailyBalanceReportAsync(DateTime startDate, DateTime endDate);
    }
}
