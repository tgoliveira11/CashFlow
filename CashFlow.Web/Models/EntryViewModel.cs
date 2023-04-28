namespace CashFlow.Web.Models
{
    public class EntryViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public EntryType Type { get; set; }
        public string? Description { get; set; }
    }

    public enum EntryType
    {
        Debit,
        Credit
    }

}