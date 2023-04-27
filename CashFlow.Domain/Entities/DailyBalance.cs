using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Entities
{
    public class DailyBalance
    {
        public DateTime Date { get; }
        public decimal TotalDebits { get; }
        public decimal TotalCredits { get; }
        public decimal NetBalance { get; }

        public DailyBalance(DateTime date, decimal totalDebits, decimal totalCredits)
        {
            Date = date;
            TotalDebits = totalDebits;
            TotalCredits = totalCredits;
            NetBalance = totalCredits - totalDebits;
        }
    }
}
