using Newtonsoft.Json;
using System;

namespace CashFlow.Web.Models
{
    public class DailyBalanceReportViewModel
    {
        
        public DateTime Date { get; }
        public decimal TotalDebits { get; }
        public decimal TotalCredits { get; }
        public decimal NetBalance { get; }

        public DailyBalanceReportViewModel(DateTime date, decimal totalDebits, decimal totalCredits)
        {
            Date = date;
            TotalDebits = totalDebits;
            TotalCredits = totalCredits;
            NetBalance = totalCredits - totalDebits;
        }
    }
}