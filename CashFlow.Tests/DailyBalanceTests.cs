using CashFlow.Domain.Entities;
using System;
using Xunit;

namespace CashFlow.Tests.Domain.Entities
{
    public class DailyBalanceTests
    {
        [Fact]
        public void ConstructDailyBalance_WithValidValues_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            DateTime date = new DateTime(2023, 5, 16);
            decimal totalDebits = 100;
            decimal totalCredits = 200;

            // Act
            DailyBalance dailyBalance = new DailyBalance(date, totalDebits, totalCredits);

            // Assert
            Assert.Equal(date, dailyBalance.Date);
            Assert.Equal(totalDebits, dailyBalance.TotalDebits);
            Assert.Equal(totalCredits, dailyBalance.TotalCredits);
            Assert.Equal(totalCredits - totalDebits, dailyBalance.NetBalance);
        }

        [Theory]
        [InlineData(-50, 200)]
        [InlineData(100, -50)]
        public void ConstructDailyBalance_WithNegativeValues_ShouldThrowArgumentException(decimal totalDebits, decimal totalCredits)
        {
            // Arrange
            DateTime date = new DateTime(2023, 5, 16);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new DailyBalance(date, totalDebits, totalCredits));
        }

        [Fact]
        public void ConstructDailyBalance_WithFutureDate_ShouldThrowArgumentException()
        {
            // Arrange
            DateTime futureDate = DateTime.UtcNow.AddDays(1);
            decimal totalDebits = 100;
            decimal totalCredits = 200;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new DailyBalance(futureDate, totalDebits, totalCredits));
        }
    }
}
