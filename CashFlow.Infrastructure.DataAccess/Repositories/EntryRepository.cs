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

        public async Task<IEnumerable<Entry>> GetAllEntriesAsync()
        {
            return await _dbContext.Entries.ToListAsync();
        }

        public async Task<Entry> GetEntryByIdAsync(Guid id)
        {
            return await _dbContext.Entries.FindAsync(id);
        }

        public async Task AddEntryAsync(Entry entry)
        {
            await _dbContext.Entries.AddAsync(entry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateEntryAsync(Entry entry)
        {
            _dbContext.Entry(entry).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEntryAsync(Guid id)
        {
            var entry = await _dbContext.Entries.FindAsync(id);
            if (entry != null)
            {
                _dbContext.Entries.Remove(entry);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
