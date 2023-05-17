using CashFlow.Api.Controllers;
using CashFlow.Api.Exceptions;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CashFlow.Domain.Entities.Entry;

namespace CashFlow.Tests
{
    public class EntriesControllerTests
    {
        private readonly Mock<IEntryRepository> _mockEntryRepository;
        private readonly EntriesController _entriesController;

        public EntriesControllerTests()
        {
            _mockEntryRepository = new Mock<IEntryRepository>();
            _entriesController = new EntriesController(_mockEntryRepository.Object);
        }

        [Fact]
        public async Task GetAllEntries_ShouldReturnAllEntries()
        {
            // Arrange
            var entries = new List<Entry>
        {
            new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit, "Test Credit"),
            new Entry(Guid.NewGuid(), DateTime.Now, 50, EntryType.Debit, "Test Debit"),
        };

            _mockEntryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(entries);

            // Act
            var result = await _entriesController.GetAllEntries();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Entry>>(actionResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task GetEntryById_ShouldReturnEntry_IfItExists()
        {
            // Arrange
            var entry = new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit, "Test Credit");

            _mockEntryRepository.Setup(repo => repo.GetByIdAsync(entry.Id)).ReturnsAsync(entry);

            // Act
            var result = await _entriesController.GetEntryById(entry.Id);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<Entry>(actionResult.Value);
            Assert.Equal(entry.Id, model.Id);
        }

        [Fact]
        public async Task GetEntryById_ShouldThrowException_IfEntryDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockEntryRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Entry)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EntryNotFoundException>(() => _entriesController.GetEntryById(id));
            Assert.Equal($"Entry with ID '{id}' not found.", exception.Message);
        }

        [Fact]
        public async Task CreateEntry_ShouldReturnCreatedAtAction_WhenModelIsValid()
        {
            // Arrange
            var entry = new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit, "Test Credit");

            _mockEntryRepository.Setup(repo => repo.AddAsync(entry)).Returns(Task.CompletedTask);

            // Act
            var result = await _entriesController.CreateEntry(entry);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_entriesController.GetEntryById), actionResult.ActionName);
            var model = Assert.IsAssignableFrom<Entry>(actionResult.Value);
            Assert.Equal(entry.Id, model.Id);
        }

        [Fact]
        public async Task UpdateEntry_ShouldReturnNoContent_WhenModelIsValid()
        {
            // Arrange
            var entry = new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit, "Test Credit");

            _mockEntryRepository.Setup(repo => repo.GetByIdAsync(entry.Id)).ReturnsAsync(entry);
            _mockEntryRepository.Setup(repo => repo.UpdateAsync(entry)).Returns(Task.CompletedTask);

            // Act
            var result = await _entriesController.UpdateEntry(entry.Id, entry);

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteEntry_ShouldReturnNoContent_WhenEntryExists()
        {
            // Arrange
            var entry = new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit, "Test Credit");

            _mockEntryRepository.Setup(repo => repo.GetByIdAsync(entry.Id)).ReturnsAsync(entry);
            _mockEntryRepository.Setup(repo => repo.DeleteAsync(entry.Id)).Returns(Task.CompletedTask);

            // Act
            var result = await _entriesController.DeleteEntry(entry.Id);

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task CreateEntry_ShouldThrowValidationException_WhenModelStateIsInvalid()
        {
            // Arrange
            var entry = new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit, "Test Credit");

            _entriesController.ModelState.AddModelError("Error", "Invalid model state.");

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => _entriesController.CreateEntry(entry));
            Assert.Equal("Invalid entry data.", ex.Message);
        }

        [Fact]
        public async Task UpdateEntry_ShouldReturnBadRequest_WhenIdDoesNotMatchModelId()
        {
            // Arrange
            var entryId = Guid.NewGuid();
            var entry = new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit, "Test Credit");

            // Act
            var result = await _entriesController.UpdateEntry(entryId, entry);

            // Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateEntry_ShouldThrowEntryNotFoundException_WhenEntryDoesNotExist()
        {
            // Arrange
            var entry = new Entry(Guid.NewGuid(), DateTime.Now, 100, EntryType.Credit, "Test Credit");

            _mockEntryRepository.Setup(repo => repo.GetByIdAsync(entry.Id)).ReturnsAsync((Entry)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<EntryNotFoundException>(() => _entriesController.UpdateEntry(entry.Id, entry));
            Assert.Equal($"Entry with ID '{entry.Id}' not found.", ex.Message);
        }

        [Fact]
        public async Task DeleteEntry_ShouldThrowEntryNotFoundException_WhenEntryDoesNotExist()
        {
            // Arrange
            var entryId = Guid.NewGuid();

            _mockEntryRepository.Setup(repo => repo.GetByIdAsync(entryId)).ReturnsAsync((Entry)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<EntryNotFoundException>(() => _entriesController.DeleteEntry(entryId));
            Assert.Equal($"Entry with ID '{entryId}' not found.", ex.Message);
        }

    }

}
