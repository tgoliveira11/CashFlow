using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Entities
{
    public class Report
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public IReadOnlyList<DailyBalance> DailyBalances { get; }

        public Report(DateTime startDate, DateTime endDate, List<DailyBalance> dailyBalances)
        {
            if (endDate < startDate)
                throw new ArgumentException("End date must be later than start date", nameof(endDate));

            if (dailyBalances == null)
                throw new ArgumentNullException(nameof(dailyBalances));

            StartDate = startDate;
            EndDate = endDate;
            DailyBalances = dailyBalances.AsReadOnly();
        }
    }

}
