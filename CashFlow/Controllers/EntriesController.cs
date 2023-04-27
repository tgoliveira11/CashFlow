using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [ApiController]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Entry>> GetEntryById(Guid id)
        {
            var entry = await _entryRepository.GetEntryByIdAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEntry(Entry entry)
        {
            await _entryRepository.AddEntryAsync(entry);
            return CreatedAtAction(nameof(GetEntryById), new { id = entry.Id }, entry);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEntry(Guid id, Entry entry)
        {
            if (id != entry.Id)
            {
                return BadRequest();
            }

            await _entryRepository.UpdateEntryAsync(entry);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEntry(Guid id)
        {
            await _entryRepository.DeleteEntryAsync(id);
            return NoContent();
        }
    }
}
