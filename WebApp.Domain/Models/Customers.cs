using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApp.Domain.Models
{
    public class Customers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("username")]
        public string UserName { get; set; } = null!;

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("address")]
        public string Address { get; set; } = null!;

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonElement("birthdate")]
        public DateTime Birthdate { get; set; }


        /*
active true

accounts
Array
0 371138
1 324287
2 276528

tier_and_details
Object 0df078f33aa74a2e9696e0520c1a828a
Object 
tier "Bronze"
id "0df078f33aa74a2e9696e0520c1a828a"
active true

benefits
Array
0 "sports tickets"
         */
    }
}
