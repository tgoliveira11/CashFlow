using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Entities
{
    public class Report
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DailyBalance> DailyBalances { get; set; }

        public Report(DateTime startDate, DateTime endDate, List<DailyBalance> dailyBalances)
        {
            StartDate = startDate;
            EndDate = endDate;
            DailyBalances = dailyBalances;
        }
    }
}
