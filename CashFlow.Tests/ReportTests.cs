using CashFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace CashFlow.Tests.Domain.Entities
{
    public class ReportTests
    {
        [Fact]
        public void ConstructReport_WithValidValues_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            DateTime startDate = new DateTime(2023, 5, 1);
            DateTime endDate = new DateTime(2023, 5, 31);
            List<DailyBalance> dailyBalances = new List<DailyBalance>
            {
                new DailyBalance(new DateTime(2023, 5, 1), 100, 200),
                new DailyBalance(new DateTime(2023, 5, 2), 50, 150),
                new DailyBalance(new DateTime(2023, 5, 3), 200, 300)
            };

            // Act
            Report report = new Report(startDate, endDate, dailyBalances);

            // Assert
            Assert.Equal(startDate, report.StartDate);
            Assert.Equal(endDate, report.EndDate);
            Assert.Equal(dailyBalances, report.DailyBalances);
        }

        [Fact]
        public void ConstructReport_WithEndDateEarlierThanStartDate_ShouldThrowArgumentException()
        {
            // Arrange
            DateTime startDate = new DateTime(2023, 5, 1);
            DateTime endDate = new DateTime(2023, 4, 30);
            List<DailyBalance> dailyBalances = new List<DailyBalance>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Report(startDate, endDate, dailyBalances));
        }

        [Fact]
        public void ConstructReport_WithNullDailyBalances_ShouldThrowArgumentNullException()
        {
            // Arrange
            DateTime startDate = new DateTime(2023, 5, 1);
            DateTime endDate = new DateTime(2023, 5, 31);
            List<DailyBalance> dailyBalances = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Report(startDate, endDate, dailyBalances));
        }
    }
}
