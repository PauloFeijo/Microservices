using Microservice.Query.Domain.Dtos;
using Microservice.Query.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly ILogger<EntryController> _logger;
        private readonly IConfiguration _config;

        public EntryController(
            IEntryRepository repo, ILogger<EntryController> logger, IConfiguration config)
        {
            _repo = repo;
            _logger = logger;
            _config = config;
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

        [HttpGet("connection_string")]
        public IActionResult GetConnectionString()
        {
            return Ok(_config.GetConnectionString("MicroservicesDb"));
        }
    }
}
