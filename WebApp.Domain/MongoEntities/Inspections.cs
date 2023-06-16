using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApp.Domain.MongoEntities
{
    [BsonIgnoreExtraElements]
    public class Inspections: IEntity
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }
        //public string? Id { get; set; }

        [BsonElement("certificate_number")]
        public string CertificateNumber { get; set; } = null!;

        [BsonElement("business_name")]
        public string BusinessName { get; set; } = null!;

        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
