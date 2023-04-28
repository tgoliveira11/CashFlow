using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Services;
using CashFlow.Intrastructure.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CashFlow.Domain.Entities.Entry;

namespace CashFlow.Tests
{
    public class ReportServiceTests
    {
        private readonly IReportService _reportService;
        private readonly Mock<IEntryRepository> _mockEntryRepository;

        public ReportServiceTests()
        {
            var entries = new List<Entry>
        {
            new Entry { Id = Guid.NewGuid(), Date = DateTime.Now, Amount = 100, Type = EntryType.Credit, Description = "Test Credit" },
            new Entry { Id = Guid.NewGuid(), Date = DateTime.Now, Amount = 50, Type = EntryType.Debit, Description = "Test Debit" },
        };

            _mockEntryRepository = new Mock<IEntryRepository>();
            _mockEntryRepository.Setup(repo => repo.GetAllEntriesAsync()).ReturnsAsync(entries);

            _reportService = new ReportService(_mockEntryRepository.Object);
        }

        [Fact]
        public async Task GenerateDailyBalanceReportAsync_ReturnsCorrectReport()
        {
            // Arrange
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.Now.Date;

            // Act
            var report = await _reportService.GenerateDailyBalanceReportAsync(startDate, endDate);

            // Assert
            _mockEntryRepository.Verify(repo => repo.GetAllEntriesAsync(), Times.Once());
            Assert.Single(report.DailyBalances);
            Assert.Equal(50, report.DailyBalances[0].NetBalance);
        }

        [Fact]
        public async Task GenerateDailyBalanceReportAsync_ReturnsEmptyReport_WhenNoEntries()
        {
            // Arrange
            _mockEntryRepository.Setup(repo => repo.GetAllEntriesAsync()).ReturnsAsync(new List<Entry>());
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.Now.Date;

            // Act
            var report = await _reportService.GenerateDailyBalanceReportAsync(startDate, endDate);

            // Assert
            _mockEntryRepository.Verify(repo => repo.GetAllEntriesAsync(), Times.Once());
            Assert.Single(report.DailyBalances);
            Assert.Equal(0, report.DailyBalances.FirstOrDefault().NetBalance);
        }

        [Fact]
        public async Task GenerateDailyBalanceReportAsync_ReturnsReportWithMultipleDays()
        {
            // Arrange
            var startDate = DateTime.Now.Date.AddDays(-2);
            var endDate = DateTime.Now.Date;
            var entries = new List<Entry>
            {
                new Entry { Id = Guid.NewGuid(), Date = startDate, Amount = 100, Type = EntryType.Credit, Description = "Test Credit 1" },
                new Entry { Id = Guid.NewGuid(), Date = endDate, Amount = 200, Type = EntryType.Credit, Description = "Test Credit 2" },
            };
            _mockEntryRepository.Setup(repo => repo.GetAllEntriesAsync()).ReturnsAsync(entries);

            // Act
            var report = await _reportService.GenerateDailyBalanceReportAsync(startDate, endDate);

            // Assert
            _mockEntryRepository.Verify(repo => repo.GetAllEntriesAsync(), Times.Once());
            Assert.Equal(3, report.DailyBalances.Count);
            Assert.Equal(100, report.DailyBalances[0].NetBalance);
            Assert.Equal(0, report.DailyBalances[1].NetBalance);
            Assert.Equal(200, report.DailyBalances[2].NetBalance);
        }

        [Fact]
        public async Task GenerateDailyBalanceReportAsync_ReturnsCorrectReport_WithCreditsAndDebits()
        {
            // Arrange
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.Now.Date;
            var entries = new List<Entry>
            {
                new Entry { Id = Guid.NewGuid(), Date = startDate, Amount = 100, Type = EntryType.Credit, Description = "Test Credit 1" },
                new Entry { Id = Guid.NewGuid(), Date = startDate, Amount = 50, Type = EntryType.Debit, Description = "Test Debit 1" },
                new Entry { Id = Guid.NewGuid(), Date = startDate, Amount = 20, Type = EntryType.Debit, Description = "Test Debit 2" },
            };
            _mockEntryRepository.Setup(repo => repo.GetAllEntriesAsync()).ReturnsAsync(entries);

            // Act
            var report = await _reportService.GenerateDailyBalanceReportAsync(startDate, endDate);

            // Assert
            _mockEntryRepository.Verify(repo => repo.GetAllEntriesAsync(), Times.Once());
            Assert.Single(report.DailyBalances);
            Assert.Equal(30, report.DailyBalances[0].NetBalance);
        }

    }
}
