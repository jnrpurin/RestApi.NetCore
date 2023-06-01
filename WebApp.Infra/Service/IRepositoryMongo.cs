using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Infra.Service
{
    public interface IRepositoryMongo<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task Add(T item);
        Task Edit(T item);
        Task Delete(int id);
    }
}
