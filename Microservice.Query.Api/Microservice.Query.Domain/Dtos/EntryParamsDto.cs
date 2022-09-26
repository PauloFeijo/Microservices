using System;

namespace Microservice.Query.Domain.Dtos
{
    public class EntryParamsDto
    {
        public string UserName { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
        public char? Type { get; set; }
        public string AccountDescription { get; set; }
        public string Description { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;
    }
}
