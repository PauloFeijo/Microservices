using System;

namespace Microservice.Query.Domain.Dtos
{
    public class EntryParamsDto
    {
        public string UserName { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Type { get; set; }
        public string AccountDescription { get; set; }
        public string Description { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 100;
    }
}
