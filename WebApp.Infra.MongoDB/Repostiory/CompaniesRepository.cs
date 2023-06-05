using MongoDB.Driver;
using WebApp.Domain.MongoEntities;
using WebApp.Infra.MongoDB.Settings;
using Microsoft.Extensions.Options;
using System.Data;

namespace WebApp.Infra.MongoDB.Repostiory
{
    public interface ICompaniesRepository 
    {
        Task<List<Companies>> GetAsync(int nroLimit);
        Task<Companies?> GetAsync(string id);
        Task CreateAsync(Companies newCompanies);
        Task UpdateAsync(string id, Companies updatedCompanies);
        Task RemoveAsync(string id);
    }

    public class CompaniesRepository: ICompaniesRepository
    {
        private readonly IMongoCollection<Companies> _companiesCollection;

        public CompaniesRepository(
            IOptions<CompanieSettings> CompanieDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                CompanieDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                CompanieDatabaseSettings.Value.DatabaseName);

            _companiesCollection = mongoDatabase.GetCollection<Companies>(
                CompanieDatabaseSettings.Value.CompanieCollectionName);
        }

        public async Task<List<Companies>> GetAsync(int nroLimit)
        {
            var limit = nroLimit > 0 ? nroLimit : 1;
            return await _companiesCollection.Find(_ => true).Limit(limit).ToListAsync();
        }

        public async Task<Companies?> GetAsync(string id) =>
            await _companiesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Companies newCompanies) =>
            await _companiesCollection.InsertOneAsync(newCompanies);

        public async Task UpdateAsync(string id, Companies updatedCompanies) =>
            await _companiesCollection.ReplaceOneAsync(x => x.Id == id, updatedCompanies);

        public async Task RemoveAsync(string id) =>
            await _companiesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
