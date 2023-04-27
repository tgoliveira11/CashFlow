using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Entities
{
    public class Entry
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public EntryType Type { get; set; }
        public string? Description { get; set; }

        public enum EntryType
        {
            Debit,
            Credit
        }
    }
}
