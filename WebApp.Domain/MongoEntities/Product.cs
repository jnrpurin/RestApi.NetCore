using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApp.Domain.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("permalink")]
        public string PermaLink { get; set; } = null!;
    }
}
