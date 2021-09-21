using Microservice.Producer.Domain.Entities;
using Microservice.Producer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microservice.Producer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntryController : ControllerBase
    {
        private readonly IEntryService _service;

        public EntryController(IEntryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Entry entry)
        {
            await _service.PublishEntry(entry);
            return Ok(entry);
        }
    }
}
