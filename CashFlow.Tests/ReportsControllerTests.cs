using CashFlow.Api.Controllers;
using CashFlow.Api.Exceptions;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CashFlow.Tests
{
    public class ReportsControllerTests
    {
        private readonly Mock<IReportService> _mockReportService;
        private readonly ReportsController _reportsController;

        public ReportsControllerTests()
        {
            _mockReportService = new Mock<IReportService>();
            _reportsController = new ReportsController(_mockReportService.Object);
        }

        [Fact]
        public async Task GetDailyBalanceReport_ShouldReturnReport()
        {
            // Arrange
            var startDate = DateTime.Now.Date;
            var endDate = startDate.AddDays(2);
            var report = new Report(startDate, endDate, new List<DailyBalance>());

            _mockReportService.Setup(service => service.GenerateReportAsync(startDate, endDate)).ReturnsAsync(report);

            // Act
            var result = await _reportsController.GetDailyBalanceReport(startDate, endDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedReport = Assert.IsType<Report>(okResult.Value);
            Assert.Equal(report, returnedReport);
        }

        [Fact]
        public async Task GetDailyBalanceReport_ShouldThrowValidationException_WhenDatesAreMinValue()
        {
            // Arrange
            var startDate = DateTime.MinValue;
            var endDate = DateTime.MinValue;

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _reportsController.GetDailyBalanceReport(startDate, endDate));
            Assert.Equal("Start date and end date are required.", ex.Message);
        }

        [Fact]
        public async Task GetDailyBalanceReport_ReturnsReport()
        {
            // Arrange
            var startDate = DateTime.Now.Date;
            var endDate = DateTime.Now.Date.AddDays(1);
            var report = new Report(startDate, endDate, Array.Empty<DailyBalance>().ToList());
            _mockReportService.Setup(service => service.GenerateReportAsync(startDate, endDate))
                .ReturnsAsync(report);

            // Act

            var result = await _reportsController.GetDailyBalanceReport(startDate, endDate);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.Equal(report, okResult.Value);
        }

        [Fact]
        public async Task GetDailyBalanceReport_ThrowsValidationException_WhenStartDateMissing()
        {
            //Arrange
            var report = new Report(DateTime.MinValue, DateTime.MinValue, new List<DailyBalance>());
            _mockReportService.Setup(service => service.GenerateReportAsync(DateTime.MinValue, DateTime.MinValue)).ReturnsAsync(report);

            // Act
            Task<ActionResult<Report>> Action() => _reportsController.GetDailyBalanceReport(DateTime.MinValue, DateTime.Now.Date);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(Action);
        }

    }

}
