using MongoDB.Driver;
using WebApp.Domain.MongoEntities;
using WebApp.Infra.MongoDB.Settings;

namespace WebApp.Infra.MongoDB.Repostiory
{
    public interface IInspectionsRepository 
    {
        Inspections GetInspectionsById(string id);
        IEnumerable<Inspections> GetInspections(string businessName);
        long UpsertInspections(Guid id, Inspections inspections);
    }
    
    public class InspectionsRepository : Repository<Inspections>, IInspectionsRepository
    {
        public InspectionsRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
        {
        }

        protected override string DefaultCollectionName => "inspections";

        public Inspections GetInspectionsById(string id) => FindOne(i => i.Id.ToString() == id) ?? new Inspections();

        public IEnumerable<Inspections> GetInspections(string businessName)
        {
            return Find(i => i.BusinessName.ToUpper().Contains(businessName.ToUpper()));
        }

        public long UpsertInspections(Guid id, Inspections inspections)
        {
            var newInspections = FindOne(i => i.Id == id);
            
            if (newInspections != null)
            {
                var  updateDefinitios = Builders<Inspections>.Update
                        .Set(x => x.BusinessName, inspections.BusinessName)
                        .Set(x => x.Date, inspections.Date);
                var filter = Builders<Inspections>.Filter.Eq(i => i.Id, newInspections.Id);
                
                return UpdateOne(filter, updateDefinitios);
            }
            else
            {
                Add(inspections);
                return 1;
            }
        }

    }
}
