using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Entities
{
    public class Entry
    {
        public Guid Id { get; }
        public DateTime Date { get; }
        public decimal Amount { get; }
        public EntryType Type { get; }
        public string? Description { get; set; }

        public Entry(Guid id, DateTime date, decimal amount, EntryType type, string? description = null)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount should be greater than zero", nameof(amount));

            Id = id;
            Date = date;
            Amount = amount;
            Type = type;
            Description = description;
        }

        public enum EntryType
        {
            Debit,
            Credit
        }
    }
}
