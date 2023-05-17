using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Entities
{
    /// <summary>
    /// Immutable Object:
    /// Validation:
    /// </summary>

    public class DailyBalance
    {
        public DateTime Date { get; }
        public decimal TotalDebits { get; }
        public decimal TotalCredits { get; }
        public decimal NetBalance { get; }

        public DailyBalance(DateTime date, decimal totalDebits, decimal totalCredits)
        {
            if (totalDebits < 0)
                throw new ArgumentException("Total debits should be greater than or equal to zero", nameof(totalDebits));

            if (totalCredits < 0)
                throw new ArgumentException("Total credits should be greater than or equal to zero", nameof(totalCredits));

            if (date > DateTime.UtcNow)
                throw new ArgumentException("Date cannot be a future date", nameof(date));

            Date = date;
            TotalDebits = totalDebits;
            TotalCredits = totalCredits;
            NetBalance = totalCredits - totalDebits;
        }
    }

}
