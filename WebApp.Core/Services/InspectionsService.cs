using WebApp.Domain.MongoEntities;
using WebApp.Infra.MongoDB.Repostiory;

namespace WebApp.Core.Services
{
    public class InspectionsService
    {
        private readonly IInspectionsRepository inspectionsRepository;

        public InspectionsService(IInspectionsRepository inspectionsRepository)
        {
            this.inspectionsRepository = inspectionsRepository;
        }

        public Inspections GetAnInspections(string id) => inspectionsRepository.GetInspectionsById(id);

        public IEnumerable<Inspections> GetInspections(string bussinesName) => inspectionsRepository.GetInspections(bussinesName);

    }
}
