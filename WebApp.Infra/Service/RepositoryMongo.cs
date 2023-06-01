using MongoDB.Driver;
using WebApp.Domain.Entity;

namespace WebApp.Infra.Service
{
    public class RepositoryMongo : IRepositoryMongo<Product>
    {
        private static Dictionary<int, Product> Products = new();

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await Task.Run(() => Products.Values.ToList());
        }

        public async Task<Product> Get(int id)
        {
            return await Task.Run(() => Products.GetValueOrDefault(id));
        }

        public async Task Add(Product Product)
        {
            await Task.Run(() => Products.Add(Product.Id, Product));
        }

        public async Task Edit(Product Product)
        {
            await Task.Run(() =>
            {
                Products.Remove(Product.Id);
                Products.Add(Product.Id, Product);
            });
        }

        public async Task Delete(int id)
        {
            await Task.Run(() => Products.Remove(id));
        }

    }

    public class ProductRepository : IRepositoryMongo<Product>
    {

        private Dictionary<int, Product> produtos = new Dictionary<int, Product>();
        public Dictionary<int, Product> GetProdutos()
        {
            produtos.Add(1, new Product { Id = 1, Name = "Caneta", Price = 3.45m });
            produtos.Add(2, new Product { Id = 2, Name = "Caderno", Price = 7.65m });
            produtos.Add(3, new Product { Id = 3, Name = "Borracha", Price = 1.20m });
            return produtos;
        }
        
        public ProductRepository()
        {
            produtos = GetProdutos();
        }

        private Task<IEnumerable<Product>> IRepositoryMongo<Product>.GetAll()
        {
            throw new NotImplementedException();
        }

        private Task<Product> IRepositoryMongo<Product>.Get(int id)
        {
            throw new NotImplementedException();
        }

        private Task IRepositoryMongo<Product>.Add(Product item)
        {
            throw new NotImplementedException();
        }

        private Task IRepositoryMongo<Product>.Edit(Product item)
        {
            throw new NotImplementedException();
        }

        private Task IRepositoryMongo<Product>.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}