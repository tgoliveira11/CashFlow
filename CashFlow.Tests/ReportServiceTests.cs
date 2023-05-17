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
            new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit,   "Test Credit"),
            new Entry(Guid.NewGuid(), DateTime.Now, 50, EntryType.Debit,   "Test Debit"),
        };

            _mockEntryRepository = new Mock<IEntryRepository>();
            _mockEntryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(entries);

            _reportService = new ReportService(_mockEntryRepository.Object);
        }

        [Fact]
        public async Task GenerateDailyBalanceReportAsync_ReturnsCorrectReport()
        {
            // Arrange
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.Now.Date;

            var entries = new List<Entry>
            {
                new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit,   "Test Credit"),
                new Entry(Guid.NewGuid(), DateTime.Now, 50, EntryType.Debit,   "Test Debit"),
            };

            _mockEntryRepository.Setup(repo => repo.GetByDateRangeAsync(startDate, endDate)).ReturnsAsync(entries);

            // Act
            var report = await _reportService.GenerateReportAsync(startDate, endDate);

            // Assert
            _mockEntryRepository.Verify(repo => repo.GetByDateRangeAsync(startDate, endDate), Times.Once());
            Assert.Single(report.DailyBalances);
            Assert.Equal(50, report.DailyBalances[0].NetBalance);
        }

        [Fact]
        public async Task GenerateDailyBalanceReportAsync_ReturnsEmptyReport_WhenNoEntries()
        {
            // Arrange
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.Now.Date;

            _mockEntryRepository.Setup(repo => repo.GetByDateRangeAsync(startDate, endDate)).ReturnsAsync(new List<Entry>());

            // Act
            var report = await _reportService.GenerateReportAsync(startDate, endDate);

            // Assert
            _mockEntryRepository.Verify(repo => repo.GetByDateRangeAsync(startDate, endDate), Times.Once());
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
                new Entry (Guid.NewGuid(), startDate, 100,  EntryType.Credit,  "Test Credit 1" ),
                new Entry (Guid.NewGuid(), endDate, 200, EntryType.Credit,  "Test Credit 2" ),
            };
            _mockEntryRepository.Setup(repo => repo.GetByDateRangeAsync(startDate, endDate)).ReturnsAsync(entries);

            // Act
            var report = await _reportService.GenerateReportAsync(startDate, endDate);

            // Assert
            _mockEntryRepository.Verify(repo => repo.GetByDateRangeAsync(startDate, endDate), Times.Once());
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
                new Entry (Guid.NewGuid(), startDate, 100, EntryType.Credit, "Test Credit 1" ),
                new Entry (Guid.NewGuid(), startDate, 50, EntryType.Debit, "Test Debit 1" ),
                new Entry (Guid.NewGuid(), startDate, 20, EntryType.Debit, "Test Debit 2" ),
            };
            _mockEntryRepository.Setup(repo => repo.GetByDateRangeAsync(startDate, endDate)).ReturnsAsync(entries);

            // Act
            var report = await _reportService.GenerateReportAsync(startDate, endDate);

            // Assert
            _mockEntryRepository.Verify(repo => repo.GetByDateRangeAsync(startDate, endDate), Times.Once());
            Assert.Single(report.DailyBalances);
            Assert.Equal(30, report.DailyBalances[0].NetBalance);
        }

        [Fact]
        public async Task GenerateDailyBalanceReportAsync_ThrowsException_WhenStartDateGreaterThanEndDate()
        {
            // Arrange
            var startDate = DateTime.Now.Date;
            var endDate = startDate.AddDays(-1);

            // Act and Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _reportService.GenerateReportAsync(startDate, endDate));
            Assert.Equal("End date must be later than start date (Parameter 'endDate')", exception.Message);
        }


        [Fact]
        public async Task GenerateDailyBalanceReportAsync_ReturnsCorrectReport_WhenEntriesAreOnDifferentDates()
        {
            // Arrange
            var startDate = DateTime.Now.Date.AddDays(-1);
            var endDate = DateTime.Now.Date;
            var entries = new List<Entry>
        {
            new Entry(Guid.NewGuid(), startDate, 100, EntryType.Credit, "Test Credit 1"),
            new Entry(Guid.NewGuid(), endDate, 50, EntryType.Debit, "Test Debit 1")
        };
            _mockEntryRepository.Setup(repo => repo.GetByDateRangeAsync(startDate, endDate)).ReturnsAsync(entries);

            // Act
            var report = await _reportService.GenerateReportAsync(startDate, endDate);

            // Assert
            _mockEntryRepository.Verify(repo => repo.GetByDateRangeAsync(startDate, endDate), Times.Once());
            Assert.Equal(2, report.DailyBalances.Count);
            Assert.Equal(100, report.DailyBalances[0].NetBalance);
            Assert.Equal(-50, report.DailyBalances[1].NetBalance);
        }


    }
}
