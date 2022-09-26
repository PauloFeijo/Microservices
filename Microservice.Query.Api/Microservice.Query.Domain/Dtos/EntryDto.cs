using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Microservice.Query.Domain.Dtos
{
    public class EntryDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime Moment { get; set; }
        public decimal Value { get; set; }
        public char Type { get; set; }
        public string AccountDescription { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
