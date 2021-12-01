using Microservice.Producer.Api.Filters;
using Microservice.Producer.Api.Models;
using Microservice.Producer.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Producer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [CustomExceptionFilter]
    public class EntryController : ControllerBase
    {
        private readonly IEntryService _service;

        public EntryController(IEntryService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] EntryModelRequest entryRequest)
        {
            var entry = entryRequest.ToEntry();
            _service.PublishEntry(entry);
            return Accepted(entryRequest);
        }
    }
}
