using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApp.Domain.Models;
using WebApp.Infra.Context;

namespace WebApp.Infra.Service
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customers> customers;

        public CustomerService(
            IOptions<MongoDbSettings> customerMongoDbSettings)
        {
            var mongoClient = new MongoClient(
                customerMongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                customerMongoDbSettings.Value.DatabaseName);

            customers = mongoDatabase.GetCollection<Customers>(
                customerMongoDbSettings.Value.CollectionName);
        }

        public async Task<List<Customers>> GetAsync() =>
            await customers.Find(_ => true).ToListAsync();

        public async Task<Customers?> GetAsync(string id) =>
            await customers.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Customers newCustomer) =>
            await customers.InsertOneAsync(newCustomer);

        public async Task UpdateAsync(string id, Customers updatedCustomer) =>
            await customers.ReplaceOneAsync(x => x.Id == id, updatedCustomer);

        public async Task RemoveAsync(string id) =>
            await customers.DeleteOneAsync(x => x.Id == id);
    }
}
