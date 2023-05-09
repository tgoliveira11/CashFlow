using CashFlow.Api.Exceptions;
using CashFlow.Api.Filters;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CashFlow.Api.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [Route("api/[controller]")]
    public class EntriesController : ControllerBase
    {
        private readonly IEntryRepository _entryRepository;

        public EntriesController(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entry>>> GetAllEntries()
        {
            return Ok(await _entryRepository.GetAllEntriesAsync());
        }

        [HttpGet("error")]
        public async Task<ActionResult<IEnumerable<Entry>>> GetAllEntriesError()
        {
            throw new Exception("testing error middleware");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entry>> GetEntryById(Guid id)
        {
            var entry = await _entryRepository.GetEntryByIdAsync(id);
            return entry == null ? throw new EntryNotFoundException($"Entry with ID '{id}' not found.") : (ActionResult<Entry>)Ok(entry);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEntry(Entry entry)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException("Invalid entry data.");
            }

            // Check for duplicate entries
            var existingEntry = await _entryRepository.GetEntryByIdAsync(entry.Id);
            if (existingEntry != null)
            {
                throw new DuplicateEntryException("An entry with the same ID already exists.");
            }

            await _entryRepository.AddEntryAsync(entry);
            return CreatedAtAction(nameof(GetEntryById), new { id = entry.Id }, entry);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEntry(Guid id, Entry entry)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException("Invalid entry data.");
            }

            if (id != entry.Id)
            {
                return BadRequest();
            }

            //validate if entry already exists on database 
            _ = await _entryRepository.GetEntryByIdAsync(entry.Id) ?? throw new EntryNotFoundException($"Entry with ID '{id}' not found.");

            await _entryRepository.UpdateEntryAsync(entry);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEntry(Guid id)
        {
            //validate if entry already exists on database 
            _ = await _entryRepository.GetEntryByIdAsync(id) ?? throw new EntryNotFoundException($"Entry with ID '{id}' not found.");

            await _entryRepository.DeleteEntryAsync(id);
            return NoContent();
        }
    }
}
