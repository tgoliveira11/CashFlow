using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly CashFlowDbContext _dbContext;

        public EntryRepository(CashFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Entry>> GetAllAsync()
        {
            return await _dbContext.Entries.ToListAsync();
        }

        public async Task<Entry> GetByIdAsync(Guid id)
        {
            return await _dbContext.Entries.FindAsync(id);
        }

        public async Task AddAsync(Entry entry)
        {
            await _dbContext.Entries.AddAsync(entry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Entry entry)
        {
            _dbContext.Entry(entry).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entry = await _dbContext.Entries.FindAsync(id);
            if (entry != null)
            {
                _dbContext.Entries.Remove(entry);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Entry>> GetByDateAsync(DateTime date)
        {
            return await _dbContext.Entries
                .Where(e => e.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Entry>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Entries
            .Where(e => e.Date.Date >= startDate.Date && e.Date.Date <= endDate.Date)
            .ToListAsync();
        }

        public async Task<IEnumerable<Entry>> GetByTypeAsync(Entry.EntryType type)
        {
            return await _dbContext.Entries
            .Where(e => e.Type == type)
            .ToListAsync();
        }

        public async Task ReplaceEntryAsync(Entry newEntry)
        {
            // Find the old entry.
            var oldEntry = await _dbContext.Entries.FindAsync(newEntry.Id);

            // If the old entry exists, remove it.
            if (oldEntry != null)
            {
                _dbContext.Entries.Remove(oldEntry);
            }

            // Add the new entry.
            await _dbContext.Entries.AddAsync(newEntry);

            // Save the changes to the database.
            await _dbContext.SaveChangesAsync();
        }
    }
}
