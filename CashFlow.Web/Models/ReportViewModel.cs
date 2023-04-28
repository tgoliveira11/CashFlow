namespace CashFlow.Web.Models
{
    public class ReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DailyBalanceReportViewModel> DailyBalances { get; set; }

        public ReportViewModel(DateTime startDate, DateTime endDate, List<DailyBalanceReportViewModel> dailyBalances)
        {
            StartDate = startDate;
            EndDate = endDate;
            DailyBalances = dailyBalances;
        }
    }
}
