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
        Task<IEnumerable<Entry>> GetAllAsync();
        Task<Entry> GetByIdAsync(Guid id);
        Task AddAsync(Entry entry);
        Task UpdateAsync(Entry entry);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Entry>> GetByDateAsync(DateTime date);
        Task<IEnumerable<Entry>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Entry>> GetByTypeAsync(Entry.EntryType type);
        Task ReplaceEntryAsync(Entry newEntry);
    }

}
