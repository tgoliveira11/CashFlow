// CashFlow.Tests/Repositories/EntryRepositoryTests.cs
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using CashFlow.Domain.Entities;
using static CashFlow.Domain.Entities.Entry;
using CashFlow.Api.Exceptions;

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
            var entry1 = new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit, "Test Credit 1");
            var entry2 = new Entry(Guid.NewGuid(), DateTime.Now, 50, EntryType.Debit, "Test Debit 1");

            _context.Entries.AddRange(entry1, entry2);
            await _context.SaveChangesAsync();

            // Act
            var entries = await _repository.GetAllAsync();

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
            var entry = new Entry(entryId, DateTime.Now, 100, EntryType.Credit, "Test Credit 1");

            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();

            // Act
            var foundEntry = await _repository.GetByIdAsync(entryId);

            // Assert
            Assert.NotNull(foundEntry);
            Assert.Equal(entryId, foundEntry.Id);
        }


        [Fact]
        public async Task AddEntryAsync_AddsEntryToDbSet()
        {
            // Arrange
            var newEntry = new Entry(Guid.NewGuid(), DateTime.Now, 200, EntryType.Credit, "Test Credit 2");

            // Act
            await _repository.AddAsync(newEntry);
            var addedEntry = await _context.Entries.FindAsync(newEntry.Id);

            // Assert
            Assert.NotNull(addedEntry);
            Assert.Equal(newEntry.Id, addedEntry.Id);
        }

        [Fact]
        public async Task ReplaceEntryAsync_ReplacesEntry()
        {
            // Arrange
            var entryId = Guid.NewGuid();
            var oldEntry = new Entry(entryId, DateTime.Now, 100, EntryType.Credit, "Test Credit 1");

            _context.Entries.Add(oldEntry);
            await _context.SaveChangesAsync();

            var newEntry = new Entry(entryId, DateTime.Now, 150, EntryType.Debit, "Updated Debit 1");

            // Act
            await _repository.ReplaceEntryAsync(newEntry);
            var replacedEntry = await _context.Entries.FindAsync(entryId);

            // Assert
            Assert.NotNull(replacedEntry);
            Assert.Equal(newEntry.Id, replacedEntry.Id);
            Assert.Equal(newEntry.Amount, replacedEntry.Amount);
            Assert.Equal(newEntry.Type, replacedEntry.Type);
            Assert.Equal(newEntry.Description, replacedEntry.Description);
        }

        [Fact]
        public async Task GetEntryByIdAsync_ReturnsNull_WhenEntryDoesNotExist()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act
            var entry = await _repository.GetByIdAsync(nonExistingId);

            // Assert
            Assert.Null(entry);
        }

        [Fact]
        public async Task DeleteEntryAsync_DeletesEntry()
        {
            // Arrange
            var entry = new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit, "Test Credit 1");
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteAsync(entry.Id);
            var deletedEntry = await _context.Entries.FindAsync(entry.Id);

            // Assert
            Assert.Null(deletedEntry);
        }

    }

}