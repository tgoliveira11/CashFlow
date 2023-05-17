using System;
using global::CashFlow.Domain.Entities;
using Xunit;

namespace CashFlow.Tests.Domain.Entities
{
    public class EntryTests
    {
        [Fact]
        public void Constructor_ValidParameters_SetsProperties()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime date = DateTime.Now;
            decimal amount = 100;
            Entry.EntryType type = Entry.EntryType.Credit;
            string description = "Test Description";

            // Act
            Entry entry = new Entry(id, date, amount, type, description);

            // Assert
            Assert.Equal(id, entry.Id);
            Assert.Equal(date, entry.Date);
            Assert.Equal(amount, entry.Amount);
            Assert.Equal(type, entry.Type);
            Assert.Equal(description, entry.Description);
        }

        [Fact]
        public void Constructor_InvalidAmount_ThrowsArgumentException()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime date = DateTime.Now;
            decimal amount = -100;
            Entry.EntryType type = Entry.EntryType.Credit;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Entry(id, date, amount, type));
        }

        [Fact]
        public void Constructor_NullDescription_SetsDescriptionToNull()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime date = DateTime.Now;
            decimal amount = 100;
            Entry.EntryType type = Entry.EntryType.Credit;

            // Act
            Entry entry = new Entry(id, date, amount, type);

            // Assert
            Assert.Null(entry.Description);
        }

        [Fact]
        public void EntryType_EnumValuesAreCorrect()
        {
            Assert.Equal(Entry.EntryType.Debit, (Entry.EntryType)0);
            Assert.Equal(Entry.EntryType.Credit, (Entry.EntryType)1);
        }
    }
}