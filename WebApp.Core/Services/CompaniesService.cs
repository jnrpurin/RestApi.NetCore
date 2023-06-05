using WebApp.Core.Interface;
using WebApp.Domain.MongoEntities;
using WebApp.Infra.MongoDB.Repostiory;

namespace WebApp.Core.Services
{
    public class CompaniesService
    {
        private readonly ICompaniesRepository _companiesRepository;

        public CompaniesService(ICompaniesRepository companiesRepository)
        {
            _companiesRepository = companiesRepository;
        }

        public async Task<IEnumerable<Companies>> GetNCompanies(int nroCompanies) => await _companiesRepository.GetAsync(nroCompanies);

        public async Task<Companies?> GetCompanieById(string id) => await _companiesRepository.GetAsync(id);

        public async Task CreateCompanie(Companies companies) => await _companiesRepository.CreateAsync(companies);

        public async Task UpdateCompanie(string id, Companies companies) => await _companiesRepository.UpdateAsync(id, companies);

        public async Task DeleteCompanie(string id) => await _companiesRepository.RemoveAsync(id);
        
    }
}
