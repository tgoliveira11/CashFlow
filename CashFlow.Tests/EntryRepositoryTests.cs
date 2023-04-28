// CashFlow.Tests/Repositories/EntryRepositoryTests.cs
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using CashFlow.Domain.Entities;
using static CashFlow.Domain.Entities.Entry;


namespace CashFlow.Tests
{
    public class EntryRepositoryTests : IDisposable
    {
        private readonly CashFlowDbContext _context;
        private readonly EntryRepository _repository;

        public EntryRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CashFlowDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new CashFlowDbContext(options);
            _repository = new EntryRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task GetAllEntriesAsync_ReturnsAllEntries()
        {
            // Arrange
            var entry1 = new Entry { Id = Guid.NewGuid(), Date = DateTime.Now, Amount = 100, Type = EntryType.Credit, Description = "Test Credit 1" };
            var entry2 = new Entry { Id = Guid.NewGuid(), Date = DateTime.Now, Amount = 50, Type = EntryType.Debit, Description = "Test Debit 1" };

            _context.Entries.AddRange(entry1, entry2);
            await _context.SaveChangesAsync();

            // Act
            var entries = await _repository.GetAllEntriesAsync();

            // Assert
            Assert.Equal(2, entries.Count());
            Assert.Contains(entry1, entries);
            Assert.Contains(entry2, entries);
        }

        [Fact]
        public async Task GetEntryByIdAsync_ReturnsEntry()
        {
            // Arrange
            var entryId = Guid.NewGuid();
            var entry = new Entry { Id = entryId, Date = DateTime.Now, Amount = 100, Type = EntryType.Credit, Description = "Test Credit 1" };

            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();

            // Act
            var foundEntry = await _repository.GetEntryByIdAsync(entryId);

            // Assert
            Assert.NotNull(foundEntry);
            Assert.Equal(entryId, foundEntry.Id);
        }

        [Fact]
        public async Task AddEntryAsync_AddsEntryToDbSet()
        {
            // Arrange
            var newEntry = new Entry { Id = Guid.NewGuid(), Date = DateTime.Now, Amount = 200, Type = EntryType.Credit, Description = "Test Credit 2" };

            // Act
            await _repository.AddEntryAsync(newEntry);
            var addedEntry = await _context.Entries.FindAsync(newEntry.Id);

            // Assert
            Assert.NotNull(addedEntry);
            Assert.Equal(newEntry.Id, addedEntry.Id);
        }

        [Fact]
        public async Task UpdateEntryAsync_UpdatesEntry()
        {
            // Arrange
            var entryId = Guid.NewGuid();
            var entry = new Entry { Id = entryId, Date = DateTime.Now, Amount = 100, Type = EntryType.Credit, Description = "Test Credit 1" };

            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();

            entry.Amount = 150;
            entry.Description = "Updated Credit 1";

            // Act
            await _repository.UpdateEntryAsync(entry);
            var updatedEntry = await _context.Entries.FindAsync(entryId);

            // Assert

        }
    }

}