using CashFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Repositories
{
    public interface IEntryRepository
    {
        Task<IEnumerable<Entry>> GetAllEntriesAsync();
        Task<Entry> GetEntryByIdAsync(Guid id);
        Task AddEntryAsync(Entry entry);
        Task UpdateEntryAsync(Entry entry);
        Task DeleteEntryAsync(Guid id);
    }
}
