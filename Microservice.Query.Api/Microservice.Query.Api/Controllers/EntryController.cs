using Microservice.Query.Domain.Dtos;
using Microservice.Query.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Microservice.Query.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntryController : ControllerBase
    {
        private readonly IEntryRepository _repo;

        public EntryController(
            IEntryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] EntryParamsDto param)
        {
            var entries = await _repo.GetEntries(param);
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntry(Guid id)
        {
            var entry = await _repo.GetEntry(id);

            if (entry == null)
            {
                return NoContent();
            }
            return Ok(entry);
        }
    }
}
