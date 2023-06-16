using MongoDB.Driver;
using System.Linq.Expressions;
using WebApp.Domain.MongoEntities;
using WebApp.Infra.MongoDB.Settings;

namespace WebApp.Infra.MongoDB.Repostiory
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Add(T entity);

        void Update(T entity);

        bool Delete(Guid id);

        T Get(Guid Id);
    }

    public abstract class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class, IEntity
    {
        protected Repository(IMongoDbContext mongoDbContext) : base(mongoDbContext) { }

        public virtual void Add(T entity)
        {
            GetCollection().InsertOne(entity);
        }

        public virtual bool Delete(Guid id)
        {
            return DeleteOne(x => x.Id == id);
        }

        public virtual T Get(Guid Id)
        {
            return FindOne(x => x.Id == Id);
        }

        public virtual void Update(T entity)
        {
            GetCollection().ReplaceOne(x => x.Id == entity.Id, entity);
        }
    }

    public abstract class RepositoryBase<T>
    {
        protected readonly IMongoDbContext mongoDbContext;

        protected RepositoryBase(IMongoDbContext mongoDbContext)
        {
            this.mongoDbContext = mongoDbContext;
        }

        protected abstract string DefaultCollectionName { get; }

        public virtual IMongoCollection<T> GetCollection()
        {
            return mongoDbContext.GetCollection<T>(DefaultCollectionName);
        }

        public virtual IEnumerable<T> Find(FilterDefinition<T> filter)
        {
            return GetCollection().Find(filter).ToEnumerable();
        }
        public virtual IEnumerable<T> Find(Expression<Func<T, Boolean>> predicate)
        {
            return GetCollection().Find(predicate).ToEnumerable();
        }

        public virtual T FindOne(Expression<Func<T, Boolean>> predicate)
        {
            return GetCollection().Find(predicate).Limit(1).FirstOrDefault();
        }
        public virtual T FindOne(FilterDefinition<T> filter)
        {
            return GetCollection().Find(filter).Limit(1).FirstOrDefault();
        }

        public virtual bool DeleteOne(Expression<Func<T, Boolean>> predicate)
        {
            return GetCollection().DeleteOne(predicate).DeletedCount == 1;
        }
        public virtual bool DeleteOne(FilterDefinition<T> filter)
        {
            return GetCollection().DeleteOne(filter).DeletedCount == 1;
        }

        public virtual long DeleteMany(FilterDefinition<T> filter)
        {
            return GetCollection().DeleteMany(filter).DeletedCount;
        }
        public virtual long DeleteMany(Expression<Func<T, Boolean>> predicate)
        {
            return GetCollection().DeleteMany(predicate).DeletedCount;
        }

        public virtual long UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition)
        {
            return GetCollection().UpdateMany(filter, updateDefinition).MatchedCount;
        }
        public virtual long UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> updateDefinition)
        {
            return GetCollection().UpdateOne(filter, updateDefinition).MatchedCount;
        }
    }
}
